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

public partial class Admin_Controls_DocUpload : System.Web.UI.UserControl
{
    private string url;
    public string Url
    {
        get { return TbFileUrl.Text.Trim(); }
        set { url = value; }
    }

    private string houzhui;
    public string Houzhui
    {
        get { return TbHouZhui.Text.Trim(); }
        set { houzhui = value; }
    }

    private string directoryUrl;
    public string DirectoryUrl
    {
        get { return directoryUrl; }
        set { directoryUrl = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
        }  
    }

    //上传
    protected void BtnUpLoad_Click(object sender, EventArgs e)
    {        
        if(FileUploadFiles.HasFile)
        {
            ArrayList arrylist = new ArrayList();
            arrylist.Add("txt");
            arrylist.Add("lnk");
            arrylist.Add("pdf");
            arrylist.Add("doc");
            arrylist.Add("docx");
            arrylist.Add("xls");
            arrylist.Add("ppt");

            ArrayList alType = new ArrayList();
            alType.Add("application/txt");
            alType.Add("application/pdf");
            alType.Add("application/msword");
            alType.Add("application/vnd.ms-excel ");
            alType.Add("application/vnd.ms-powerpoint ");
            alType.Add("application/docx");
            alType.Add("application/pptx");       
           

            string fileName=FileUploadFiles.FileName;
            string[] houzhuiList=fileName.Split(new char[]{'.'});
            string houzhui=houzhuiList[houzhuiList.Length-1].ToLower();

            if (arrylist.Contains(houzhui))
            {
                //if (alType.Contains(FileUploadFiles.PostedFile.ContentType))
                //{
                    if (FileUploadFiles.PostedFile.ContentLength / (1024 * 1024) > 20)
                    {
                        LbTishi.Text = "上传文件过大";
                    }
                    else
                    {
                        #region

                        string strRand = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString();//日期
                        Random rand = new Random();
                        strRand += rand.Next(10000000, 99999999);//随机数

                        string ImgName = strRand + "." + houzhui;//文件名称

                        if (directoryUrl == null || directoryUrl == string.Empty)
                        {
                            directoryUrl = "Userfiles/";
                        }

                        //核查目录，如果不存在就创建该目录
                        if (!Directory.Exists(Server.MapPath("~/" + directoryUrl)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/" + directoryUrl));
                        }

                        string strImgUrl = Server.MapPath("~/" + directoryUrl) + ImgName;//上传路径

                        FileUploadFiles.SaveAs(strImgUrl);//上传

                        //保存到数据库的路径
                        string strSqlUrl = directoryUrl + ImgName;

                        TbFileUrl.Visible = true;
                        FileUploadFiles.Visible = false;
                        BtnUpLoad.Visible = false;
                        BtnDelete.Visible = true;
                        //TbFileUrl.Enabled = false;

                        TbFileUrl.Text = strSqlUrl;
                        url = strSqlUrl;
                        TbHouZhui.Text = houzhui;

                        #endregion
                    }
                //}
                //else
                //{
                //    LbTishi.Text = "请上传正确的文件";
                //}
            }
            else
            {
                LbTishi.Text = "文件类型错误";
            }
        }  
    }

    //删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        if (directoryUrl == null || directoryUrl == string.Empty)
        {
            directoryUrl = "~/Userfiles/";
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
        BtnDelete.Visible = false;
    }
}
