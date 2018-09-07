using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web;

namespace YK.Common
{
    /// <summary>
    /// 地址帮助类
    /// </summary>
    public class AddressHelper
    {
        public static string xmlUrl {
            get {
                string path = null;
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    path = HttpContext.Current.Server.MapPath("~/xml/Address.xml");
                }
                else
                {
                    path = System.Windows.Forms.Application.StartupPath + @"\xml\Address.xml";
                }
                return path;
            }
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public static string GetProvince(string code)
        {
            string result = String.Empty;
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlUrl);
                XmlNodeList xnl = xmldoc.SelectSingleNode("country").ChildNodes;
                string json = "[";
                foreach(XmlNode xn in xnl)
                {
                    json += "{code"+":"+xn.Attributes["code"].Value + ",name:\"" + xn.Attributes["name"].Value + "\"},";
                }
                json = json.TrimEnd(',')+"]";
                return json.TrimEnd(',');
            }
            catch (Exception ex)
            {
                //写入日志
                TxtFileHelper.AppendLogTxt(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 获取市、县
        /// </summary>
        /// <param name="code">省份编号</param>
        /// <returns></returns>
        public static string GetCity(string code)
        {
            string result = String.Empty;
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlUrl);
                XmlNodeList xnl = xmldoc.SelectSingleNode("country").ChildNodes;
                string json = "[";
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Attributes["code"].Value == code)
                    {
                        XmlNodeList citylist = xn.ChildNodes;
                        foreach (XmlNode city in citylist)
                        {
                            json += "{code" + ":" + city.Attributes["code"].Value + ",name:\"" + city.Attributes["name"].Value + "\"},";                        
                        }
                    }
                }
                json = json.TrimEnd(',') + "]";
                return json.TrimEnd(',');
            }
            catch (Exception ex)
            {
                //写入日志
                TxtFileHelper.AppendLogTxt(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 获取区
        /// </summary>
        /// <param name="code">城市编号</param>
        /// <returns></returns>
        public static string GetArea(string code)
        {
            string result = String.Empty;
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlUrl);
                XmlNodeList xnl = xmldoc.SelectSingleNode("country").ChildNodes;
                string json = "[";
                foreach (XmlNode xn in xnl)
                {
                    XmlNodeList citylist = xn.ChildNodes;
                    foreach (XmlNode city in citylist)
                    {
                        if (city.Attributes["code"].Value == code)
                        {
                            XmlNodeList arealist = city.ChildNodes;
                            foreach (XmlNode area in arealist)
                            {
                                json += "{code" + ":" + area.Attributes["code"].Value + ",name:\"" + area.Attributes["name"].Value + "\"},";
                            }
                        }
                    }
                }
                json = json.TrimEnd(',') + "]";
                return json.TrimEnd(',');
            }
            catch (Exception ex)
            {
                //写入日志
                TxtFileHelper.AppendLogTxt(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 获取省、市、区名称
        /// </summary>
        /// <param name="code">省、市、区编号</param>
        /// <returns></returns>
        public static string[] GetNames(string codes)
        {
            string[] codeSp = codes.Split(',');
            string[] names=new string[codeSp.Length];

            string result = String.Empty;
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlUrl);
                XmlNodeList xnl = xmldoc.SelectSingleNode("country").ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Attributes["code"].Value == codeSp[0])
                    {
                        names[0] = xn.Attributes["name"].Value;

                        XmlNodeList citylist = xn.ChildNodes;
                        foreach (XmlNode city in citylist)
                        {
                            if (city.Attributes["code"].Value == codeSp[1])
                            {
                                names[1] = city.Attributes["name"].Value;

                                XmlNodeList arealist = city.ChildNodes;
                                foreach (XmlNode area in arealist)
                                {
                                    if (area.Attributes["code"].Value == codeSp[2])
                                    {
                                        names[2] = area.Attributes["name"].Value;
                                    }
                                }

                            }
                        }
                    }
                }                
                return names;
            }
            catch (Exception ex)
            {
                //写入日志
                TxtFileHelper.AppendLogTxt(ex.Message);
                throw ex;
            }
        }
    }
}
