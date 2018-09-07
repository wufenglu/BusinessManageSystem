using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace YK.Common
{
    /// <summary>
    /// 文本处理类
    /// </summary>
    public class TxtFileHelper
    {
        private static object lockobj = new object();

        /// <summary>
        /// 追加写入文本
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="text">内容</param>
        public static void AppendWriteTxt(string filePath, string text)
        {
            System.IO.File.AppendAllText(filePath, text);
        }

        /// <summary>
        /// 追加写入文本
        /// </summary>
        /// <param name="text">内容</param>
        public static void AppendLogTxt(string text)
        {
            lock (lockobj) {
                //文本
                text = "\r\n========================" + DateTime.Now.ToString()
                        + "========================\r\n" + text;
                //目录
                string directory = "";
                //http对象
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    directory = HttpContext.Current.Server.MapPath("~/logs/");
                }
                else
                {
                    directory = System.Windows.Forms.Application.StartupPath + @"\logs\";
                }
                //检查目录是否存在，如果不存在则创建
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                //文件路径
                string filePath = directory + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                //追加文本
                System.IO.File.AppendAllText(filePath, text);
            }
        }
    }
}
