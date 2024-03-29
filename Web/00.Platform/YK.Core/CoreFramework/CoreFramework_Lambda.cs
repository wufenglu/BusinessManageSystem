using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Json;

using YK.Core.Model;
using YK.Utility.Extensions;
using System.Linq.Expressions;

namespace YK.Core.CoreFramework
{
    /// <summary>
    /// 公共操作类，Lambda模块（Lambda）
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    internal partial class CoreFramework<TEntity>
    { 
        /// <summary>
        /// 参数数组
        /// </summary>        
        List<SqlParameter> listPara = new List<SqlParameter>();
        /// <summary>
        /// 随机数
        /// </summary>
        Random random = new Random();

        /// <summary>
        /// 获取Lambda表达式返回的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public CoreFrameworkEntity GetLambdaEntity<T>(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            listPara.Clear();
            CoreFrameworkEntity lambdaEntity = new CoreFrameworkEntity();

            // 二元运算符表达式
            if (func.Body is System.Linq.Expressions.BinaryExpression)
            {
                System.Linq.Expressions.BinaryExpression be = ((System.Linq.Expressions.BinaryExpression)func.Body);
                lambdaEntity.Where = BinarExpressionProvider(be.Left, be.Right, be.NodeType);
                lambdaEntity.ParaList = listPara;                
            }
            // 单纯的静态方法
            if (func.Body is System.Linq.Expressions.MethodCallExpression)
            {
                System.Linq.Expressions.MethodCallExpression be = ((System.Linq.Expressions.MethodCallExpression)func.Body);
                lambdaEntity.Where = ExpressionRouter(be);
                lambdaEntity.ParaList = listPara;                
            }
            return lambdaEntity;
        }

        /// <summary>
        /// 二元运算符表达式
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string BinarExpressionProvider(System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right, System.Linq.Expressions.ExpressionType type)
        {
            string where = "(";
            //先处理左边
            string leftStr = ExpressionRouter(left);  
            //获取实体列的特性
            List<EntityPropColumnAttributes> columnAttrList = AttributeHelper.GetEntityColumnAtrributes<TEntity>();
            var list = columnAttrList.Where(w => w.propName == leftStr);
            if (list.Count() > 0)
            {
                EntityPropColumnAttributes columnAttribute = list.First();
                leftStr = columnAttribute.fieldName;
            }
            //节点类型
            string typeStr = ExpressionTypeCast(type);            

            //再处理右边
            string rightStr = RightExpressionRouter(right).ToStr();

            where += leftStr;
            where += typeStr;

            if (rightStr == "null")
            {
                if (where.EndsWith(" ="))
                    where = where.Substring(0, where.Length - 2) + " is null";
                else if (where.EndsWith("<>"))
                    where = where.Substring(0, where.Length - 2) + " is not null";
            }
            else
            {
                //如果左侧包含（则代表左侧非字段
                if (leftStr.Contains("("))
                {
                    where += rightStr;                    
                }
                else
                {
                    int num = random.Next(100, 999);
                    where += "@" + leftStr + num;
                    listPara.Add(new SqlParameter("@" + leftStr + num, rightStr));
                }
            }
            return where += ")";
        }

        /// <summary>
        /// 表达式路由
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        string ExpressionRouter(System.Linq.Expressions.Expression exp)
        {
            //获取实体列的特性
            List<EntityPropColumnAttributes> columnAttrList = AttributeHelper.GetEntityColumnAtrributes<TEntity>();            

            string sb = string.Empty;
            if (exp is System.Linq.Expressions.BinaryExpression)//二元运算符
            {
                System.Linq.Expressions.BinaryExpression be = ((System.Linq.Expressions.BinaryExpression)exp);
                return BinarExpressionProvider(be.Left, be.Right, be.NodeType);                
            }
            else if (exp is System.Linq.Expressions.MemberExpression)//成员
            {
                System.Linq.Expressions.MemberExpression me = ((System.Linq.Expressions.MemberExpression)exp);
                return me.Member.Name;                
            }
            else if (exp is System.Linq.Expressions.NewArrayExpression)//数组
            {
                System.Linq.Expressions.NewArrayExpression ae = ((System.Linq.Expressions.NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (System.Linq.Expressions.Expression ex in ae.Expressions)
                {
                    tmpstr.Append(ExpressionRouter(ex));
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            else if (exp is System.Linq.Expressions.MethodCallExpression)//方法
            {
                return MethodExpression(exp);
            }
            else if (exp is System.Linq.Expressions.ConstantExpression)
            {
                System.Linq.Expressions.ConstantExpression ce = ((System.Linq.Expressions.ConstantExpression)exp);
                if (ce.Value == null)
                    return "null";
                else if (ce.Value is ValueType)
                    return ce.Value.ToString();
                else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
                    return string.Format("{0}", ce.Value.ToString());
            }
            else if (exp is System.Linq.Expressions.UnaryExpression)
            {
                System.Linq.Expressions.UnaryExpression ue = ((System.Linq.Expressions.UnaryExpression)exp);
                return ExpressionRouter(ue.Operand);
            }
            return null;
        }

        /// <summary>
        /// 右边表达式路由
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        object RightExpressionRouter(System.Linq.Expressions.Expression exp)
        {
            string sb = string.Empty;
            if (exp is System.Linq.Expressions.BinaryExpression)
            {
                System.Linq.Expressions.BinaryExpression be = ((System.Linq.Expressions.BinaryExpression)exp);
                return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            else if (exp is System.Linq.Expressions.MethodCallExpression)//方法
            {
                return MethodExpression(exp);
            }
            else
            {
                return System.Linq.Expressions.Expression.Lambda(exp).Compile().DynamicInvoke();
            }            
        }

        string MethodExpression(System.Linq.Expressions.Expression exp)
        {
            System.Linq.Expressions.MethodCallExpression mce = (System.Linq.Expressions.MethodCallExpression)exp;

            string key = ExpressionRouter(mce.Arguments[0]);
            object obj = RightExpressionRouter(mce.Arguments[1]);

            //对象属性
            EntityPropColumnAttributes columnAttribute = columnAttrList.Where(w => w.propName == key).First();
            key = columnAttribute.fieldName;

            //参数名称
            string paramName = "@" + key + random.Next(1000, 9999);            
            string values = "";

            #region 拼接参数值

            if (mce.Method.Name == "Like" || mce.Method.Name == "NotLike")
            {
                listPara.Add(new SqlParameter(paramName, "%" + obj.ToStr() + "%"));
            }

            if (mce.Method.Name == "LeftLike")
            {
                listPara.Add(new SqlParameter(paramName, obj.ToStr() + "%"));
            }

            if (mce.Method.Name == "RightLike")
            {
                listPara.Add(new SqlParameter(paramName, "%" + obj.ToStr()));
            }

            if (mce.Method.Name == "In" || mce.Method.Name == "NotIn")
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                List<object> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(json);

                if (list.Count == 0)
                {
                    return " (1=2) ";
                }

                int index = 0;
                foreach (var value in list)
                {
                    index++;
                    listPara.Add(new SqlParameter(paramName + index, value.ToStr()));
                    values += paramName + index + ",";
                }
            }
            values = values.TrimEnd(',');

            #endregion

            if (mce.Method.Name == "Like" || mce.Method.Name == "LeftLike" || mce.Method.Name == "RightLike")
            {
                return string.Format("({0} like {1})", key, paramName);
            }
            else if (mce.Method.Name == "NotLike")
            {
                return string.Format("({0} Not like {1})", key, paramName);
            }
            else if (mce.Method.Name == "In")
            {
                return string.Format("{0} In ({1})", key, values);
            }
            else if (mce.Method.Name == "NotIn")
            {
                return string.Format("{0} Not In ({1})", key, values);
            }

            return " (1=2) ";
        }

        /// <summary>
        /// 表达式类型计算
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string ExpressionTypeCast(System.Linq.Expressions.ExpressionType type)
        {
            switch (type)
            {
                case System.Linq.Expressions.ExpressionType.And:
                case System.Linq.Expressions.ExpressionType.AndAlso:
                    return " AND ";
                case System.Linq.Expressions.ExpressionType.Equal:
                    return " =";
                case System.Linq.Expressions.ExpressionType.GreaterThan:
                    return " >";
                case System.Linq.Expressions.ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case System.Linq.Expressions.ExpressionType.LessThan:
                    return "<";
                case System.Linq.Expressions.ExpressionType.LessThanOrEqual:
                    return "<=";
                case System.Linq.Expressions.ExpressionType.NotEqual:
                    return "<>";
                case System.Linq.Expressions.ExpressionType.Or:
                case System.Linq.Expressions.ExpressionType.OrElse:
                    return " Or ";
                case System.Linq.Expressions.ExpressionType.Add:
                case System.Linq.Expressions.ExpressionType.AddChecked:
                    return "+";
                case System.Linq.Expressions.ExpressionType.Subtract:
                case System.Linq.Expressions.ExpressionType.SubtractChecked:
                    return "-";
                case System.Linq.Expressions.ExpressionType.Divide:
                    return "/";
                case System.Linq.Expressions.ExpressionType.Multiply:
                case System.Linq.Expressions.ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    return null;
            }
        }
    }
}
