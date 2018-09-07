using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;


using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public partial class Admin_VerifyCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string chkCode = string.Empty;
        int width = 100;
        int height = 27;
        int fontSize = 14;

        Color[] color = { Color.Black, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.DarkBlue };
        string[] font = {"Times New Roman","MS Mincho","Book Antiqua","Gungsuh","PMingLiU","Impact" };

        char[] character = {'2','3','4','5','6','8','9','1' };

        #region 生成验证码
        Random rnd = new Random();
        for (int i = 0; i < 4; i++)
        {
            chkCode += character[rnd.Next(character.Length)];
        }
        
        #endregion


        Bitmap bmp = new Bitmap(width, height);
        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);

        #region 噪线

        //for (int i = 0; i < 5; i++)
        //{
        //    int x1 = rnd.Next(width);
        //    int y1 = rnd.Next(height);
        //    int x2 = rnd.Next(width);
        //    int y2 = rnd.Next(height);
        //    Color clr = color[rnd.Next(color.Length)];

        //    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
        //}
        #endregion

        #region 画验证码
        for (int i = 0; i < chkCode.Length; i++)
        {
            string fnt = font[rnd.Next(font.Length)];
            Font ft = new Font(fnt, fontSize,FontStyle.Bold);
            Color clr = color[rnd.Next(color.Length)];
            g.DrawString(chkCode[i].ToString(),ft,new SolidBrush(clr),(float)i*20+20,(float)6);
        }

        #endregion

        #region 噪点

            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x,y,clr);
            }
        #endregion


        //清除该页面缓存
        //Response.Buffer = true;
        //Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
        //Response.Expires = 0;
        //Response.CacheControl = "no-cache";
        //Response.AppendHeader("Pragma","No-Cache");

        //将验证码图片写入内存流，并将其以image/Png格式输出

        Session["VerifyCode"] = chkCode;

        MemoryStream ms = new MemoryStream();
        try
        {
            bmp.Save(ms, ImageFormat.Png);
            Response.ClearContent();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.ToArray());

        }
        finally {
            bmp.Dispose();
            g.Dispose();
            ms.Dispose();
        
        }

    }
}
