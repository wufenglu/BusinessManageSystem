using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

namespace YK.Utility
{
    /// <summary>
    /// 通用类
    /// </summary>
    public static partial class CommonClass
    {
        /// <summary>
        /// 获取目录路径
        /// </summary>
        public static string AppPath = HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

        /// <summary>
        /// 应用程序的物理目录﻿
        /// </summary>
        public static string PhysicalApplicationPath {
            get {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    return HttpContext.Current.Request.PhysicalApplicationPath;
                }
                else
                {
                    return System.Windows.Forms.Application.StartupPath;
                }
            }
        } 

        /// <summary>
        /// 获取完整路径
        /// </summary>
        public static string FullAppPath
        {
            get
            {
                string[] arr = System.Web.HttpContext.Current.Request.Url.ToString().Split('/');
                string fullUrl = arr[0] + "//" + arr[2];
                return fullUrl.TrimEnd('/') + AppPath;
            }
        }

        /// <summary>
        /// 在地址栏设置当前参数，允许多参数
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramsDic">参数</param>
        /// <returns></returns>
        public static string SetUrlParas(string url, Dictionary<string, string> paramsDic)
        {
            string[] split = url.Split('?'); //分割判断是否存在参数
            if (split.Length > 1)
            {
                url = split[0] + "?"; //获取无参数的地址

                string[] paraList = split[1].Split('&'); //分割获取参数信息

                foreach (string info in paraList)
                {
                    string paraName = info.Split('=')[0].ToLower();//获取参数名

                    //如果有相同的参数名，则取当前的
                    if (!paramsDic.Keys.Contains(paraName))
                    {
                        url += info + "&";
                    }
                }
            }
            else
            {
                url += "?";
            }

            foreach (var item in paramsDic)
            {
                url += item.Key + "=" + item.Value + "&";
            }

            return url.TrimEnd('&');
        }

        /// <summary>
        /// 移除地址里面的参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="removeParas"></param>
        /// <returns></returns>
        public static string RemoveUrlParas(string url, string removeParas)
        {
            string[] deleteParas = removeParas.ToLower().Split(',');

            string[] split = url.Split('?'); //分割判断是否存在参数
            if (split.Length > 1)
            {
                url = split[0] + "?"; //获取无参数的地址

                string[] paraList = split[1].Split('&'); //分割获取参数信息

                foreach (string info in paraList)
                {
                    string paraName = info.Split('=')[0].ToLower();//获取参数名

                    //该参数是否在需要被移除的参数列表里面
                    if (!deleteParas.Contains(paraName))
                    {
                        url += info + "&";
                    }
                }  
            }

            return url.TrimEnd('&');
        }

        /// <summary>
        /// 将实体列表转换为数据表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable EntityListToDataTable<Tentity>(List<Tentity> list) where Tentity : new()
        {
            //实体
            Tentity entity = new Tentity();
            //数据表
            DataTable dt = new DataTable(entity.GetType().Name);
            //创建列
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = prop.Name;
                dc.DataType = typeof(string);
                dt.Columns.Add(dc);
            }
            //填充行
            foreach (Tentity model in list)
            {
                DataRow dr = dt.NewRow();
                //循环属性
                foreach (PropertyInfo prop in model.GetType().GetProperties())
                {
                    string typeName = prop.PropertyType.Name;
                    dr[prop.Name] = prop.GetValue(model, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        
   }
}
