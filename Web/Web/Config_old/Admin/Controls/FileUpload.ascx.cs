using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;

public partial class Admin_Controls_FileUpload : System.Web.UI.UserControl
{
    /// <summary>
    /// 文件路径
    /// </summary>
    private string url;
    public string Url
    {
        get { return TbFileUrl.Text.Trim(); }
        set { url = value; }
    }

    /// <summary>
    /// 上传目录
    /// </summary>
    private string directoryUrl;
    public string DirectoryUrl
    {
        get { return directoryUrl; }
        set { directoryUrl = value; }
    }

    /// <summary>
    /// 是否添加水印
    /// </summary>
    private bool isWaterMark;
    public bool IsWaterMark
    {
        get { return isWaterMark; }
        set { isWaterMark = value; }
    }

    /// <summary>
    /// 文件后缀
    /// </summary>
    private string fileSuffix;
    public string FileSuffix {
        get { return fileSuffix; }
        set { fileSuffix = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ImageShow.Attributes.Add("onmouseover", "LargeImgShow(this.src)");
        ImageShow.Attributes.Add("onmouseout", "LargeImgClose()");
        LoadDataBind();
    }

    //加载绑定
    public void LoadDataBind()
    {       
        if (url != null && url !=string.Empty)
        {
            FileUploadFiles.Visible = false;
            BtnUpLoad.Visible = false;
            TbFileUrl.Visible = true;            
            BtnDelete.Visible = true;
            TbFileUrl.Text = url;

            string[] houZhuiList = url.Split(new char[] {'.'});
            string houzhui = houZhuiList[houZhuiList.Length - 1];
            ArrayList arrylist = new ArrayList();
            arrylist.Add("jpg");
            arrylist.Add("jpeg");
            arrylist.Add("png");
            arrylist.Add("ico");
            arrylist.Add("gif");
            arrylist.Add("bmp");
            if (arrylist.Contains(houzhui))
            {
                ImageShow.Visible = true;
                ImageShow.Src = YK.Common.CommonClass.AppPath + url;
            }
        }  
    }

    //上传
    protected void BtnUpLoad_Click(object sender, EventArgs e)
    {     
        if(FileUploadFiles.HasFile)
        {
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
            alType.Add("image/png");
            alType.Add("image/x-icon");
            alType.Add("image/jpeg");
            alType.Add("image/x-png");
            alType.Add("application/octet-stream");
            alType.Add("application/msword");
            alType.Add("text/plain");
            alType.Add("application/x-shockwave-flash");
            alType.Add("audio/mpeg");

            string fileName=FileUploadFiles.FileName;
            string[] houzhuiList=fileName.Split(new char[]{'.'});
            string houzhui=houzhuiList[houzhuiList.Length-1].ToLower();

            if (!string.IsNullOrEmpty(fileSuffix))
            {
                List<string> suffix=new List<string>();
                suffix.AddRange(fileSuffix.Split(','));
                if (!suffix.Contains(houzhui))
                {
                    LbTishi.Text = "上传文件类型错误，请上传" + fileSuffix + "类型文件";
                    return;
                }
                else
                {
                    LbTishi.Text = "";
                }
            }

            if(arrylist.Contains(houzhui))
            {
                if(alType.Contains(FileUploadFiles.PostedFile.ContentType))
                {
                    if (FileUploadFiles.PostedFile.ContentLength / (1024 * 1024) >= 20)
                    {
                        LbTishi.Text = "上传文件过大";
                    }
                    else
                    {
                        string strRand = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString();//日期
                        Random rand = new Random();
                        strRand += rand.Next(10000000, 99999999);//随机数
                        
                        string ImgName = strRand + "." + houzhui;//文件名称
                                                
                        if(directoryUrl==null||directoryUrl==string.Empty)
                        {
                            directoryUrl = "Userfiles/";
                        }

                        //保存到数据库的路径
                        string strSqlUrl = directoryUrl + ImgName;

                        //上传路径 
                        string strImgUrl = Server.MapPath("~/"+directoryUrl) + ImgName;                     

                        //核查目录，如果不存在就创建该目录
                        if (!Directory.Exists(Server.MapPath("~/" + directoryUrl)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/" + directoryUrl));
                        }

                        FileUploadFiles.SaveAs(strImgUrl);//上传

                        #region 水印
                        if (isWaterMark == true)
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

                        TbFileUrl.Visible = true;
                        ImageShow.Visible = true;
                        FileUploadFiles.Visible = false;
                        BtnUpLoad.Visible = false;
                        BtnDelete.Visible = true;
                        //TbFileUrl.Enabled = false;

                        TbFileUrl.Text = strSqlUrl;
                        ImageShow.Src ="../../"+ strSqlUrl;
                        url = strSqlUrl;
                    }                   
                }               
            }           
        }  
    }

    //删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        if (directoryUrl == null || directoryUrl == string.Empty)
        {
            directoryUrl = "Userfiles/";
        }

        DirectoryInfo dire = new DirectoryInfo(Server.MapPath("~/" + directoryUrl));
        FileInfo[] fileInfo = dire.GetFiles();
        string[] strArray=TbFileUrl.Text.Trim().Split(new char[]{'/'});
        string ImgName=strArray[strArray.Length-1];
        foreach(FileInfo file in fileInfo)
        {
           if(file.Name==ImgName)
           {
               file.Delete();
               TbFileUrl.Text = "";
           }
        }
        FileUploadFiles.Visible = true;
        BtnUpLoad.Visible = true;
        TbFileUrl.Visible = false;
        ImageShow.Visible = false;
        BtnDelete.Visible = false;
    }
}
