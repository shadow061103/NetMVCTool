using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NetMVCTool.Models;

namespace NetMVCTool.Controllers
{
    public class APIController : Controller
    {
        //抓政府公開資料
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            string url = "http://opendata.epa.gov.tw/ws/Data/UV/?format=json";
            HttpResponseMessage response = await client.GetAsync(url);

            HttpContent content = response.Content;
            string jsonresult = await content.ReadAsStringAsync();
            var model = new List<apimodel>();
            if (jsonresult != null)
                 model = JsonConvert.DeserializeObject<List<apimodel>>(jsonresult);

            ViewBag.List = model;

            return View();
        }
    }
}