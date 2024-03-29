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
using System.ComponentModel;
using System.Data.Linq.Mapping;
using YK.Core.Model;
using YK.Utility.Extensions;

namespace YK.Core.CoreFramework
{
    /// <summary>
    /// 公共操作类，级联模块（Casecade）
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    internal partial class CoreFramework<TEntity> 
    {
        public TEntity CasecadeInsert(TEntity entity)
        {
            return SaveMainInfos(entity);
        }
        /// <summary>
        /// 保存主表信息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public TEntity SaveMainInfos(TEntity entity)
        {
            List<string> sqls = new List<string>();
            List<SqlParameter> rootParas = new List<SqlParameter>();

            string insertSql = "insert into " + entity.GetType().Name + "(";
            string valueSql = "values(";
            //分割实体名称
            string[] strList = entity.GetType().Name.ToLower().Split(new char[] { '_' });
            string primaryKey = GetPrimaryKey();
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                object value = prop.GetValue(entity, null);
                if (value != null && prop.Name.ToLower() != primaryKey)
                {
                    List<string> types = new List<string>() { "String", "Int32", "Boolean", "Byte", "Char", "Decimal", "Double", "Int64", "Object", "Int16", "Single", "DateTime" };
                    if (types.Contains(prop.PropertyType.Name))
                    {
                        insertSql += prop.Name + ",";
                        valueSql += "@" + prop.Name + ",";// value + ",";             
                        rootParas.Add(new SqlParameter("@" + prop.Name, Convert.ChangeType(value, prop.PropertyType)));
                    }
                }
            }
            insertSql = insertSql.TrimEnd(',') + ") ";
            valueSql = valueSql.TrimEnd(',') + ")";

            var sqlHelper = new SqlHelper.SqlHelper();
            sqlHelper.ExecuteNonQuery(insertSql + valueSql, rootParas);
            int id = sqlHelper.ExecuteScalar("select max(" + primaryKey + ") from " + entity.GetType().Name).ToInt();

            PropertyInfo proInfo = entity.GetType().GetProperty(primaryKey);
            proInfo.SetValue(entity, Convert.ChangeType(id, proInfo.PropertyType), null);
            //保存从表信息
            SaveChidInfos(entity);
            return entity;
        }

        /// <summary>
        /// 保存从表信息
        /// </summary>
        /// <param name="t"></param>
        public void SaveChidInfos(TEntity entity)
        {
            List<List<SqlParameter>> paraLists = new List<List<SqlParameter>>();
            List<string> sqls = new List<string>();
            string primaryKey = GetPrimaryKey();
            object primaryKeyValue = entity.GetType().GetProperty(primaryKey).GetValue(entity, null);
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                object value = prop.GetValue(entity, null);
                if (value != null && prop.Name.ToLower() != primaryKey)
                {
                    if (prop.PropertyType.IsGenericType)
                    {
                        int count = Convert.ToInt32(value.GetType().GetProperty("Count").GetValue(value, null));
                        for (int i = 0; i < count; i++)
                        {
                            List<SqlParameter> paras = new List<SqlParameter>();
                            object childValue = value.GetType().GetProperty("Item").GetValue(value, new object[] { i });
                            string childInsertSql = "insert into " + childValue.GetType().Name + "(";
                            string childValueSql = "values(";
                            string[] strChildList = value.GetType().Name.ToLower().Split(new char[] { '_' });
                            foreach (PropertyInfo childProp in childValue.GetType().GetProperties())
                            {
                                if (childProp.Name != strChildList[strChildList.Length - 1] + "id" && childProp.Name.ToLower() != "id")
                                {
                                    object[] obj = childProp.GetCustomAttributes(typeof(DescriptionAttribute), false);
                                    bool isForeignKey = false;
                                    foreach (var s in obj)
                                    {
                                        DescriptionAttribute attr = s as DescriptionAttribute;
                                        if (attr.Description == "ForeignKey")
                                        {
                                            isForeignKey = true;
                                        }
                                    }
                                    childInsertSql += childProp.Name + ",";
                                    childValueSql += "@" + childProp.Name + ",";// childProp.GetValue(childValue, null) + ",";
                                    if (isForeignKey == false)
                                    {
                                        paras.Add(new SqlParameter("@" + childProp.Name, Convert.ChangeType(childProp.GetValue(childValue, null), childProp.PropertyType)));
                                    }
                                    else
                                    {
                                        paras.Add(new SqlParameter("@" + childProp.Name, Convert.ChangeType(primaryKeyValue, childProp.PropertyType)));
                                    }
                                }
                            }
                            childInsertSql = childInsertSql.TrimEnd(',') + ") ";
                            childValueSql = childValueSql.TrimEnd(',') + ")";
                            sqls.Add(childInsertSql + childValueSql);
                            paraLists.Add(paras);
                        }
                    }
                    else
                    {
                        List<string> types = new List<string>() { "String", "Int32", "Boolean", "Byte", "Char", "Decimal", "Double", "Int64", "Object", "Int16", "Single", "DateTime" };
                        if (!types.Contains(prop.PropertyType.Name))
                        {
                            List<SqlParameter> paras = new List<SqlParameter>();
                            string childInsertSql = "insert into " + prop.PropertyType.Name + "(";
                            string childValueSql = "values(";
                            string[] strChildList = value.GetType().Name.ToLower().Split(new char[] { '_' });
                            foreach (PropertyInfo childProp in prop.PropertyType.GetProperties())
                            {
                                if (childProp.Name != strChildList[strChildList.Length - 1] + "id" && childProp.Name.ToLower() != "id")
                                {
                                    object[] obj = childProp.GetCustomAttributes(typeof(DescriptionAttribute), false);
                                    bool isForeignKey = false;
                                    foreach (var s in obj)
                                    {
                                        DescriptionAttribute attr = s as DescriptionAttribute;
                                        if (attr.Description == "ForeignKey")
                                        {
                                            isForeignKey = true;
                                        }
                                    }
                                    childInsertSql += childProp.Name + ",";
                                    childValueSql += "@" + childProp.Name + ",";// childProp.GetValue(childValue, null) + ",";                            
                                    if (isForeignKey == false)
                                    {
                                        paras.Add(new SqlParameter("@" + childProp.Name, Convert.ChangeType(childProp.GetValue(value, null), childProp.PropertyType)));
                                    }
                                    else
                                    {
                                        paras.Add(new SqlParameter("@" + childProp.Name, Convert.ChangeType(primaryKeyValue, childProp.PropertyType)));
                                    }
                                }
                            }
                            childInsertSql = childInsertSql.TrimEnd(',') + ") ";
                            childValueSql = childValueSql.TrimEnd(',') + ")";
                            sqls.Add(childInsertSql + childValueSql);
                            paraLists.Add(paras);
                        }
                    }
                }
            }
            var sqlHelper = new SqlHelper.SqlHelper();
            for (int i = 0; i < sqls.Count; i++)
            {
                sqlHelper.ExecuteNonQuery(sqls[i], paraLists[i]);
            }
        }
    }
}
