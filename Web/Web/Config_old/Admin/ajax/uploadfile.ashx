<%@ WebHandler Language="C#" Class="login" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Linq;
using System.IO;

public class Result
{
    public bool success { get; set; }
    public string message { get; set; }
}

public class login : IHttpHandler,IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        var file = context.Request.Files[0];
        var suffix = context.Request["suffix"];
        var directory = context.Request["directory"];
        var isWaterMark = context.Request["isWaterMark"];

        #region 后缀及类型
        ArrayList arrylist = new ArrayList();
        arrylist.Add("jpg");
        arrylist.Add("jpeg");
        arrylist.Add("png");
        arrylist.Add("ico");
        arrylist.Add("gif");
        arrylist.Add("bmp");
        arrylist.Add("rar");
        arrylist.Add("txt");
        arrylist.Add("doc");
        arrylist.Add("txt");
        arrylist.Add("zip");
        arrylist.Add("swf");
        arrylist.Add("mp3");
        
        ArrayList alType = new ArrayList();
        alType.Add("image/pjpeg");
        alType.Add("image/gif");
        alType.Add("image/bmp");
        alType.Add("image/x-icon");
        alType.Add("image/jpeg");
        alType.Add("image/x-png");
        alType.Add("application/octet-stream");
        alType.Add("application/msword");
        alType.Add("text/plain");
        alType.Add("application/x-shockwave-flash");
        alType.Add("audio/mpeg");

        #endregion

        Result result = new Result();
        result.success = false;

        string fileName = file.FileName;
        string[] houzhuiList = fileName.Split(new char[] { '.' });
        string houzhui = houzhuiList[houzhuiList.Length - 1].ToLower();

        if (!string.IsNullOrEmpty(suffix))
        {
            List<string> suffixs = new List<string>();
            suffixs.AddRange(suffix.Split(','));
            if (!suffix.Contains(houzhui))
            {                
                result.message = "上传文件类型错误，请上传" + suffix + "类型文件";
                context.Response.Write(YK.Common.JsonHelper.Serialize(result));
                return;
            }
        }

        if (arrylist.Contains(houzhui))
        {
            if (alType.Contains(file.ContentType))
            {
                if (file.ContentLength / (1024 * 1024) >= 20)
                {
                    result.message = "上传文件过大";
                    context.Response.Write(YK.Common.JsonHelper.Serialize(result));
                    return;
                }
                else
                {
                    string strRand = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString();//日期
                    Random rand = new Random();
                    strRand += rand.Next(10000000, 99999999);//随机数

                    string ImgName = strRand + "." + houzhui;//文件名称

                    if (directory == null || directory == string.Empty)
                    {
                        directory = "Userfiles/";
                    }

                    //上传路径 
                    string strImgUrl = context.Server.MapPath("~/" + directory);

                    //核查目录，如果不存在就创建该目录
                    if (!Directory.Exists(strImgUrl))
                    {
                        Directory.CreateDirectory(strImgUrl);
                    }
                    strImgUrl = strImgUrl + ImgName;
                    
                    file.SaveAs(strImgUrl);//上传

                    #region 水印
                    if (isWaterMark !=null && isWaterMark.ToLower() == "true")
                    {
                        string fileUrl = HttpContext.Current.Server.MapPath("~/App_Data/WebSet/WebSet.xml");
                        YK.Model.WebSet webset = YK.Common.MyXmlSerializer<YK.Model.WebSet>.Get(fileUrl);
                        switch (webset.WaterMarkType)
                        {
                            case -1:
                                break;
                            case 0:
                                YK.Common.WaterMarkHelper.TxtWaterMark(strImgUrl);
                                break;
                            case 1:
                                YK.Common.WaterMarkHelper.PicWaterMark(strImgUrl);
                                break;
                        }
                    }
                    #endregion
                    result.success = true;
                    result.message = YK.Common.BasePage.AppPath + directory + ImgName;//保存到数据库的路径
                    context.Response.Write(YK.Common.JsonHelper.Serialize(result));
                    return;
                }
            }
        }  
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}