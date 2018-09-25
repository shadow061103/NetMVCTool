using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetMVCTool.Models
{
    public class QRCodeModel
    {
        [Display(Name = "QRCode 文字")]
        public string QRCodeText { get; set; }
        [Display(Name = "QRCode 圖片")]
        public string QRCodeImagePath { get; set; }
    }
}