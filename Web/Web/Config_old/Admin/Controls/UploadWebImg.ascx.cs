using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class Admin_Controls_UploadWebImg : System.Web.UI.UserControl
{
    private string url;
    public string Url
    {
        get { return url; }
        set { url = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //上传
    protected void BtnUpLoad_Click(object sender, EventArgs e)
    {
        if(url!=string.Empty)
        {
            if (FileUploadFiles.HasFile)
            {
                //当前上传图片的后缀
                string fileName = FileUploadFiles.FileName;
                string[] houzhuiList = fileName.Split(new char[] { '.' });
                string houzhui = houzhuiList[houzhuiList.Length - 1].ToLower();

                //需要上传图片的后缀
                string[] imghouzhuiList = url.Split(new char[] { '.' });
                string imghouzhui = imghouzhuiList[imghouzhuiList.Length - 1].ToLower();

                if (houzhui == imghouzhui)
                {                        
                    string Directory = Server.MapPath(url);//上传路径

                    //核查目录，如果不存在就创建该目录
                    if (File.Exists(Directory))
                    {
                        FileUploadFiles.SaveAs(Directory);//上传
                        MsgDiv.InnerHtml = "<script>alert('上传成功');</script>";
                    }
                }
                else
                {
                    MsgDiv.InnerHtml = "<script>alert('图片类型错误，请上传" + imghouzhui + "图片');</script>";
                }
            }
            else
            {
                MsgDiv.InnerHtml = "<script>alert('请选择图片');</script>";
            }
        }        
    }
}
