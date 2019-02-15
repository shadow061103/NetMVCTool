using NetMVCTool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace NetMVCTool.Controllers
{
    public class UploadController : Controller
    {
        SqlConnection con = new SqlConnection(
              WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        // GET: Upload
        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase File)
        {
            if (File != null && File.ContentLength > 0)
            {
                //存到資料夾
                var FileName = Path.GetFileName(File.FileName);
                var FilePath = Path.Combine(Server.MapPath("~/Images/"), FileName);
                File.SaveAs(FilePath);


                //轉成byte 方法一 直接轉
                byte[] FileBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    File.InputStream.CopyTo(ms);
                    FileBytes = ms.GetBuffer();
                }
                //方法二 讀實體檔案出來再轉
                //using (var Fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                //{
                //    using (var Reader = new BinaryReader(Fs))
                //    {
                //        FileBytes = Reader.ReadBytes((int)Fs.Length);
                //    }
                //}

                //存進資料庫再取出
                InsertPhoto(FileBytes);
                FileBytes = SelectPhoto();

                TempData["Data"] = FileBytes;
               
            }


            return View();
        }
        public void InsertPhoto(byte[] photo)
        {
            string str = "Insert into ImageData(Image) values(@Image)";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.Parameters.AddWithValue(@"Image", photo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }


        }
        public byte[] SelectPhoto()
        {
            byte[] photo;
            string str = "select top 1 Image from ImageData order by ID desc";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    photo = (byte[])reader["Image"];
                    con.Close();
                    return photo;
                }
                else
                    return null;

                
            }
           

        }

    }
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString DisplayImage(this HtmlHelper helper, byte[] imageData, string alternative = "")
        {
            if (imageData != null)
            {
                string src = string.Empty;
                string base64= Convert.ToBase64String(imageData);
                src = string.Format("data:image/gif;base64,{0}", base64);
                var buildr = new TagBuilder("img");
                buildr.MergeAttribute("src", src);
                if (!string.IsNullOrEmpty(alternative))
                {
                    buildr.MergeAttribute("alt", alternative);
                }
                return MvcHtmlString.Create(buildr.ToString(TagRenderMode.SelfClosing));
            }
            return null;
        }

    }
}