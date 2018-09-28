using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMVCTool.Controllers
{
    public class ImgController : Controller
    {
        #region 產生圖形驗證碼
        // GET: Img
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(int a)
        {

            //這邊要驗證verifyCode 用model繫結跟session比較
            return View();
        }
        public ActionResult CreateVerifyCode()
        {
            string verifyCode = GetRandomNumber(5);
            Session["ValidateCode"] = verifyCode; //用於驗證
            CreateCheckCodeImage(verifyCode);


            return View();
        }
        //隨機生成數字字符串
        public string GetRandomNumber(int Length)
        {
            string str_Number = String.Empty;
            Random random = new Random();
            for (int i = 0; i <= Length; i++)
            {
                str_Number += random.Next(10).ToString(); //傳回指定最大值10隨機數字
            }
            return str_Number;
        }
        private void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
            {
                return;
            }

            Bitmap image = new Bitmap(Convert.ToInt32(Math.Ceiling((double)(checkCode.Length * 25))), 43);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成隨機生成器
                Random random = new Random();

                //清空圖片背景色
                g.Clear(Color.White);
                //for (int i = 0; i < 25; i++)
                //{

                //    //畫圖片的背景噪音線
                //    int x1 = random.Next(image.Width);
                //    int x2 = random.Next(image.Width);
                //    int y1 = random.Next(image.Height);
                //    int y2 = random.Next(image.Height);

                //    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                //}

                Font font = new Font("Comic Sans MS", 18, FontStyle.Italic);

                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Black, 1.2F, true);
                g.DrawString(checkCode, font, brush, 0, 4);
                //for (int i = 0; i < 100; i++)
                //{
                //    //畫圖片的前景噪音點
                //    int x = random.Next(image.Width);
                //    int y = random.Next(image.Height);

                //    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                //}

                //畫圖片的邊框線
                //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion


    }
}