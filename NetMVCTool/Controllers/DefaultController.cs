using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetMVCTool.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            string aa = Guid.NewGuid().ToString();
            string refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            return View();
        }
    }
}