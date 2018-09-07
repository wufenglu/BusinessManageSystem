using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace YK.Common
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 反射获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateObject<T>() where T : class
        {
            //获取类型
            Type type = typeof(T);

            //xml路径
            string path = "";
            HttpContext context = HttpContext.Current;
            if (context != null)
                path = HttpContext.Current.Server.MapPath("~/xml/");
            else
                path = System.Windows.Forms.Application.StartupPath + @"\xml\";

            //根据命名空间获取xml文件数据
            DataSet ds = new DataSet();
            ds.ReadXml(path + type.Namespace + ".xml");

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["interface"].ToString() == type.Name)
                {
                    //反射得到对象
                    return System.Reflection.Assembly.Load(dr["assembly"].ToString()).CreateInstance(dr["class"].ToString()) as T;
                }
            }
            return null;
        }

        /// <summary>
        /// 反射执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public static object AssemblyExecution<T>(string methodName, Dictionary<string, object> dicParams) where T : class
        {
            //获取类型
            Type type = typeof(T);
            //xml路径
            string path = "";
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                path = HttpContext.Current.Server.MapPath("~/xml/");
            }
            else
            {
                path = System.Windows.Forms.Application.StartupPath + @"\xml\";
            }

            //根据命名空间获取xml文件数据
            DataSet ds = new DataSet();
            ds.ReadXml(path + type.Namespace + ".xml");

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["interface"].ToString() == type.Name)
                {
                    return AssemblyExecution(dr["class"].ToString(), methodName, dicParams);
                }
            }
            return null;
            
        }

        /// <summary>
        /// 反射执行方法
        /// </summary>
        /// <param name="fullClass"></param>
        /// <param name="methodName"></param>
        /// <param name="dicParams"></param>
        /// <returns></returns>
        public static object AssemblyExecution(string fullClass, string methodName, Dictionary<string, object> dicParams)
        {
            //分割完整类名，得到程序集
            string[] arr = fullClass.Split('.');

            //程序集
            string assemblyName = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                assemblyName += arr[i] + ".";
            }
            assemblyName = assemblyName.TrimEnd('.');

            //得到对象
            var obj = Assembly.Load(assemblyName).CreateInstance(fullClass);
            //参数集合
            object[] paramsArr = new object[dicParams.Count];

            //获取方法，遍历执行需要的方法
            MethodInfo[] methods = obj.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                //参数数量相同，参数名相同
                if (method.GetParameters().Count() == dicParams.Count && method.Name == methodName)
                {
                    ParameterInfo[] paramsInfo = method.GetParameters();
                    int index = 0;
                    foreach (ParameterInfo info in paramsInfo)
                    {
                        //参数赋值
                        paramsArr[index] = Convert.ChangeType(dicParams[info.Name], info.ParameterType);
                        index++;
                    }
                    return method.Invoke(obj, paramsArr);
                }
            }
            return null;
        }
    }
}
