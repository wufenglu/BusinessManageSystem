using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using YK.Common;
using YK.Model;
using YK.Interface;

namespace YK.Common
{
    /// <summary>
    /// 公共操作类
    /// </summary>
    /// <typeparam name="Tentity">实体</typeparam>
    public class CommonDAL<Tentity>:ICommon<Tentity> where Tentity : new()
    {
        /// <summary>
        /// 插入（添加）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="PrimaryKeyName">主键名称，为空字符则无主键</param>
        /// <returns></returns>
        public int Insert(Tentity entity,string PrimaryKeyName)
        {
            //参数列表
            List<SqlParameter> listPara = new List<SqlParameter>();
            string insertStr = "";//插入字段
            string paraStr = "";//参数
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                //当属性名称不等于主键名称
                if (prop.Name != PrimaryKeyName)
                {
                    insertStr += prop.Name + ",";//字符拼接
                    paraStr += "@" + prop.Name + ",";//字符拼接
                    listPara.Add(new SqlParameter("@" + prop.Name, prop.GetValue(entity, null)));//参数添加
                }
            }
            //去掉最后的逗号
            insertStr = insertStr.TrimEnd(',');
            paraStr = paraStr.TrimEnd(',');
            //表名,即实体名称
            string tableName = entity.GetType().Name;
            //拼接SQL语句
            string cmdText = "insert into " + tableName + "(" + insertStr + ") values(" + paraStr + ")";
            return SqlHelper.ExecuteNonQuery(cmdText, listPara);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public int Update(Tentity entity, List<Expression> express)
        {
            //参数列表
            List<SqlParameter> listPara = new List<SqlParameter>();
            string setStr = "";//set修改字段和参数
            string where = "";//条件
            bool IsCondition=false;//是否作为修改的条件字段
            int count = 0;//记录作为条件的个数
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                foreach(Expression exp in express)
                {
                    if(exp.FieldName==prop.Name)
                    {
                        IsCondition = true;
                        count++;
                    }
                }
                //此属性不作为修改条件
                if (IsCondition==false)
                {
                    setStr += prop.Name + "=@" + prop.Name + ",";//字符拼接
                    listPara.Add(new SqlParameter("@" + prop.Name, prop.GetValue(entity, null)));//参数添加
                }
                //此属性作为修改条件
                if (IsCondition==true)
                {
                    if (count == express.Count)
                        where = prop.Name + "=@" + prop.Name;//条件
                    else
                        where = prop.Name + "=@" + prop.Name+" and ";//条件
                    
                    listPara.Add(new SqlParameter("@" + prop.Name,express[count-1].Value));//参数添加
                }

                //还原
                IsCondition = false;
            }
            setStr = setStr.TrimEnd(',');
            //表名,即实体名称
            string tableName = entity.GetType().Name;
            //拼接SQL语句
            string cmdText = "update " + tableName + " set " + setStr + " where " + where;
            return SqlHelper.ExecuteNonQuery(cmdText, listPara);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public int Delete(List<Expression> express)
        {
            //参数列表
            List<SqlParameter> listPara = new List<SqlParameter>();
            string where = "";//条件
            int i = 0;
            foreach (Expression exp in express)
            {
                i++;
                listPara.Add(new SqlParameter("@" + exp.FieldName, exp.Value));//参数添加
                //当为最后一个条件时
                if (i == express.Count)
                {
                    where = exp.FieldName + "=@" + exp.FieldName;//条件                    
                }
                else
                {
                    where = exp.FieldName + "=@" + exp.FieldName+" and ";//条件
                }                
            }
            //表名,即实体名称
            string tableName = new Tentity().GetType().Name;
            //拼接SQL语句
            string cmdText = "delete from " + tableName + " where " + where;
            return SqlHelper.ExecuteNonQuery(cmdText, listPara);
        }
          

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public Tentity Get(List<Expression> express)
        {
            Tentity entity = new Tentity();
            //参数列表
            List<SqlParameter> listPara = new List<SqlParameter>();
            string where = "";//条件
            int i = 0;
            foreach (Expression exp in express)
            {
                i++;
                //如果执行到最后一个，则不需要and
                if (i == express.Count)
                {
                    where = exp.FieldName +exp.Condition+ "@" + exp.FieldName;//条件
                }
                else
                {
                    where = exp.FieldName +exp.Condition+ "@" + exp.FieldName+" and ";//条件
                }
                listPara.Add(new SqlParameter("@" + exp.FieldName,exp.Value));//参数添加                               
            }
            //表名,即实体名称
            string tableName = entity.GetType().Name;
            //拼接SQL语句
            string cmdText = "select * from " + tableName + " where " + where;
            SqlDataReader sdr = SqlHelper.ExecuteReader(cmdText, listPara);
            while (sdr.Read())
            {
                //循环属性
                foreach (PropertyInfo prop2 in entity.GetType().GetProperties())
                {
                    string typeName = prop2.PropertyType.Name;
                    switch (typeName)
                    {
                        case "Int32":
                            prop2.SetValue(entity, sdr[prop2.Name].ToInt(), null);
                            break;
                        case "String":
                            prop2.SetValue(entity, sdr[prop2.Name].ToStr(), null);
                            break;
                        case "DateTime":
                            prop2.SetValue(entity, Convert.ToDateTime(sdr[prop2.Name]), null);
                            break;
                        case "Boolean":
                            prop2.SetValue(entity, sdr[prop2.Name].ToBool(), null);
                            break;
                        case "Bool":
                            prop2.SetValue(entity, sdr[prop2.Name].ToBool(), null);
                            break;
                        case "Decimal":
                            prop2.SetValue(entity, sdr[prop2.Name].ToDecimal(), null);
                            break;
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="selectFields">查询字段</param>
        /// <param name="express">表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="recordCount">数据总条数</param>        
        /// <returns></returns>
        public List<Tentity> Search(int pageSize, int pageIndex, string primaryKey, string selectFields, List<Expression> express, string orderBy, ref int recordCount)
        {
            #region 获取参数和条件
            //参数列表
            List<SqlParameter> listPara = new List<SqlParameter>();
            string where = "";//条件
            if (express != null)
            {
                int count = express.Count;//表达式的个数
                int i = 1;//运行的位置
                foreach (Expression exp in express)
                {
                    
                    #region 条件语句
                    switch (exp.Condition)
                    {
                        case "like":
                            listPara.Add(new SqlParameter("@" + exp.FieldName, "%"+exp.Value+"%"));
                            exp.Condition = " like @" + exp.FieldName;
                            break;
                        default:
                            listPara.Add(new SqlParameter("@" + exp.FieldName, exp.Value));
                            exp.Condition = exp.Condition+ "@" + exp.FieldName;
                            break;

                    }
                    #endregion
                    //如果是最后一个条件
                    if (i == express.Count)
                    {
                        where += exp.FieldName + exp.Condition;
                    }
                    else
                    {
                        where += exp.FieldName + exp.Condition + " and ";
                    }
                    i++;
                }
            }
            else
            {
                where = "1=1";
            }
            #endregion

            
            Tentity model=new Tentity();
            string tableName = model.GetType().Name;//表名
            if(string.IsNullOrEmpty(primaryKey))
            {
                primaryKey = model.GetType().GetProperties()[0].Name;//主键
            }
            selectFields=selectFields==""?"*":selectFields;//查询字段
            //排序
            orderBy = orderBy == "" ? "" : "order by " + orderBy;
            //拼接条件
            where = where + " " + orderBy;
            Pager page = new Pager();
            DataTable dt = page.GetPagerInfo(tableName, primaryKey, selectFields, pageSize, pageIndex, where, ref recordCount, listPara).Tables[0];
            
            List<Tentity> entityList = new List<Tentity>();
            #region 循环赋值
            foreach (DataRow dr in dt.Rows)
            {
                //实体
                Tentity entity = new Tentity();
                //循环属性
                foreach (PropertyInfo prop in entity.GetType().GetProperties())
                {
                    string typeName = prop.PropertyType.Name;
                    switch (typeName)
                    {
                        case "Int32":
                            prop.SetValue(entity, dr[prop.Name].ToInt(), null);
                            break;
                        case "String":
                            prop.SetValue(entity, dr[prop.Name].ToStr(), null);
                            break;
                        case "DateTime":
                            prop.SetValue(entity, Convert.ToDateTime(dr[prop.Name]), null);
                            break;
                        case "Boolean":
                            prop.SetValue(entity, dr[prop.Name].ToBool(), null);
                            break;
                        case "Bool":
                            prop.SetValue(entity, dr[prop.Name].ToBool(), null);
                            break;
                        case "Decimal":
                            prop.SetValue(entity, dr[prop.Name].ToDecimal(), null);
                            break;
                    }
                }
                entityList.Add(entity);
            }
            #endregion
            return entityList;
        
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="selectFields">查询字段</param>
        /// <param name="express">表达式</param>
        /// <param name="orderBy">排序,为空则不排序</param>
        /// <returns></returns>
        public List<Tentity> Search(string selectFields,List<Expression> express, string orderBy)
        {
            #region 获取参数和条件
            //参数列表
            List<SqlParameter> listPara = new List<SqlParameter>();
            string where = "";//条件
            if (express != null)
            {
                int count = express.Count;//表达式的个数
                int i = 1;//运行的位置
                foreach (Expression exp in express)
                {
                    #region 条件语句
                    switch (exp.Condition)
                    {
                        case "like":
                            listPara.Add(new SqlParameter("@" + exp.FieldName, "%" + exp.Value + "%"));
                            exp.Condition = " like @" + exp.FieldName;
                            break;
                        default:
                            listPara.Add(new SqlParameter("@" + exp.FieldName, exp.Value));
                            exp.Condition = exp.Condition + "@" + exp.FieldName;
                            break;

                    }
                    #endregion
                    //如果是最后一个条件
                    if (i == express.Count)
                    {
                        where += exp.FieldName + exp.Condition;
                    }
                    else
                    {
                        where += exp.FieldName + exp.Condition + " and ";
                    }
                    i++;
                }
            }
            else
            {
                where = "1=1";
            }
            #endregion

            Tentity model = new Tentity();
            string tableName = model.GetType().Name;//表名
            string primaryKey = model.GetType().GetProperties()[0].Name;//主键
            selectFields = selectFields == "" ? "*" : selectFields;//查询字段
            orderBy = orderBy == "" ? "" : "order by " + orderBy;
            string cmdText = "select "+selectFields+" from "+tableName+" where "+where +" "+orderBy;
            DataTable dt= SqlHelper.GetDataSet(cmdText,listPara).Tables[0];
            List<Tentity> entityList = new List<Tentity>();
            #region 循环赋值
            foreach (DataRow dr in dt.Rows)
            {
                //实体
                Tentity entity = new Tentity();
                //循环属性
                foreach (PropertyInfo prop in entity.GetType().GetProperties())
                {
                    string typeName = prop.PropertyType.Name;
                    switch (typeName)
                    {
                        case "Int32":
                            prop.SetValue(entity, dr[prop.Name].ToInt(), null);
                            break;
                        case "String":
                            prop.SetValue(entity, dr[prop.Name].ToStr(), null);
                            break;
                        case "DateTime":
                            prop.SetValue(entity, Convert.ToDateTime(dr[prop.Name]), null);
                            break;
                        case "Boolean":
                            prop.SetValue(entity, dr[prop.Name].ToBool(), null);
                            break;
                        case "Bool":
                            prop.SetValue(entity, dr[prop.Name].ToBool(), null);
                            break;
                        case "Decimal":
                            prop.SetValue(entity, dr[prop.Name].ToDecimal(), null);
                            break;
                    }
                }
                entityList.Add(entity);
            }
            #endregion
            return entityList;
        }
    }    
}
