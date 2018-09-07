using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using YK.Model;

namespace YK.Unity
{
    /// <summary>
    ///WaterMarkHelper 水印帮助类的摘要说明
    /// </summary>
    public class WaterMarkHelper
    {
        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="path">加水印图片的物理路径</param>
        public static void TxtWaterMark(string path)
        {
            string fileUrl = HttpContext.Current.Server.MapPath("~/App_Data/WebSet/WebSet.xml");
            WebSet webset= MyXmlSerializer<YK.Model.WebSet>.Get(fileUrl);

            Color[] color = { Color.Black, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.DarkBlue };
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            int fontSize = 14;
            string txt = webset.WaterMarkTxt;
            Random rnd = new Random();

            System.Drawing.Image image = System.Drawing.Image.FromFile(path);
            int width = image.Width;
            int height = image.Height;

            Bitmap bmp = new Bitmap(image, width, height);
            Graphics g = Graphics.FromImage(bmp);
            //g.Clear(Color.White);

            //坐标值
            int x = 0, y = 0;

            switch (webset.WaterMarkHorizontal)
            {
                case "left":
                    x = 0;
                    break;
                case "center":
                    x = (width - 20 * (txt.Length)) / 2;
                    break;
                case "right":
                    x = width - 20 * (txt.Length) - 10;
                    break;
            }

            switch (webset.WaterMarkVertical)
            {
                case "top":
                    y = 0 + 10; //向下移动10像素好看些
                    break;
                case "middle":
                    y = height / 2 - fontSize;
                    break;
                case "bottom":
                    y = height - 14;
                    break;
            }

            // 画验证码
            for (int i = 0; i < txt.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize, FontStyle.Bold);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(txt[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + x, y);
            }

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            #region 保存加水印的图片

            string testPath = HttpContext.Current.Server.MapPath("~/test2.jpg");
            byte[] imgData = ms.ToArray();
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
            FileStream fs = new FileStream(testPath, FileMode.Create, FileAccess.Write);
            if (fs != null)
            {
                fs.Write(imgData, 0, imgData.Length);
                fs.Close();
            }

            #endregion

            g.Dispose();
            bmp.Dispose();
            image.Dispose();
        }

        /// <summary>    
        /// 给图片上水印    
        /// </summary>    
        /// <param name="path">加水印图片的物理路径</param>
        public static void PicWaterMark(string filePath)
        {
            string fileUrl = HttpContext.Current.Server.MapPath("~/App_Data/WebSet/WebSet.xml");
            YK.Model.WebSet webset = MyXmlSerializer<YK.Model.WebSet>.Get(fileUrl);

            string waterFile = HttpContext.Current.Server.MapPath("~/" + webset.WaterMarkPicUrl);

            string ModifyImagePath = filePath;//修改的图像路径    
            int lucencyPercent = 25;//透明度
            Image modifyImage = null;
            Image drawedImage = null;
            Graphics g = null;

            //建立图形对象    
            modifyImage = Image.FromFile(ModifyImagePath, true);
            drawedImage = Image.FromFile(waterFile, true);
            g = Graphics.FromImage(modifyImage);
            //设置颜色矩阵    
            float[][] matrixItems ={    
            new float[] {1, 0, 0, 0, 0},    
            new float[] {0, 1, 0, 0, 0},    
            new float[] {0, 0, 1, 0, 0},    
            new float[] {0, 0, 0, (float)lucencyPercent/100f, 0},    
            new float[] {0, 0, 0, 0, 1}};

            //获取要绘制图形坐标    
            int x = 0;
            int y = 0;
            //水印图片的宽和高
            int width = drawedImage.Width;
            int height = drawedImage.Height;



            switch (webset.WaterMarkHorizontal)
            {
                case "left":
                    x = 0;
                    break;
                case "center":
                    x = (modifyImage.Width - width) / 2;
                    break;
                case "right":
                    x = modifyImage.Width - width;
                    break;
            }

            switch (webset.WaterMarkVertical)
            {
                case "top":
                    y = 0 + 10; //向下移动10像素好看些
                    break;
                case "middle":
                    y = (modifyImage.Height - height) / 2;
                    break;
                case "bottom":
                    y = modifyImage.Height - height;
                    break;
            }

            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
            ImageAttributes imgAttr = new ImageAttributes();
            imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            //绘制阴影图像    
            g.DrawImage(drawedImage,
                new Rectangle(x, y, drawedImage.Width, drawedImage.Height),
                0, 0, //水印图片的坐标值开始，如0,0就是从水印图片的0,0坐标开始画图
                drawedImage.Width,
                drawedImage.Height,
                GraphicsUnit.Pixel,
                imgAttr);
            //保存文件    
            string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
            FileInfo fi = new FileInfo(ModifyImagePath);
            ImageFormat imageType = ImageFormat.Gif;
            switch (fi.Extension.ToLower())
            {
                case ".jpg": imageType = ImageFormat.Jpeg; break;
                case ".gif": imageType = ImageFormat.Gif; break;
                case ".png": imageType = ImageFormat.Png; break;
                case ".bmp": imageType = ImageFormat.Bmp; break;
                case ".tif": imageType = ImageFormat.Tiff; break;
                case ".wmf": imageType = ImageFormat.Wmf; break;
                case ".ico": imageType = ImageFormat.Icon; break;
                default: break;
            }
            MemoryStream ms = new MemoryStream();
            modifyImage.Save(ms, imageType);
            byte[] imgData = ms.ToArray();
            drawedImage.Dispose();
            modifyImage.Dispose();
            g.Dispose();

            FileStream fs = null;
            File.Delete(ModifyImagePath);
            fs = new FileStream(ModifyImagePath, FileMode.Create, FileAccess.Write);
            if (fs != null)
            {
                fs.Write(imgData, 0, imgData.Length);
                fs.Close();
            }
        }
    }

}