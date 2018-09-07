using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace YK.Common
{
    /// <summary>
    ///翼风短信接口 的摘要说明
    /// </summary>
    public class MSMSend
    {
        public string url = "http://n.020sms.com/MSMSEND.ewing";//发送的地址
        public string code = "zengkunqiang";//企业代码
        public string userName = "zengkunqiang";//用户名
        public string userPwd = "123456";//密码

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobileList">手机号列表，以“,”号隔开</param>
        /// <param name="sendContent">发送内容</param>
        /// <returns></returns>
        public bool Send(string mobileList, string sendContent)
        {
            string para = "ECODE=" + code + "&USERNAME=" + userName
            + "&PASSWORD=" + userPwd + "&MOBILE=" + mobileList + "&CONTENT=" + sendContent;

            byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(para);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = postBytes.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
            }
            bool b = false;
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理   
                StreamReader sr = new StreamReader(wr.GetResponseStream());
                string data = sr.ReadToEnd().Trim();
                b = data == "1";
            }
            return b;
        }
    }
}
