using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ApI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            JToken Data= GetData();

            List<JToken> children= new List<JToken>();

            children = Data["children"].ToList();
            List<JToken> finaldataList = new List<JToken>();
            List<string> titleList = new List<string>();
            List<string> imageList = new List<string>();
            
            for (int i = 0; i < children.Count; i++)
            {
                JToken dataList = children[i];
                JToken data = dataList["data"];
                finaldataList.Add(data);
                string title = data["title"].ToString();
                titleList.Add(title);
                JToken preview = data["preview"];
                string image = preview["images"].ToString();
                imageList.Add(image);
            }

            List<string> finalTitleList = titleList;
            ViewBag.ttitle = finalTitleList;
            List<string> finalimageList = imageList;
            ViewBag.image = finalimageList;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public JToken GetData()
        {
            string URL = "https://www.reddit.com/r/aww/.json";
            HttpWebRequest request = WebRequest.CreateHttp(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string APIText = sr.ReadToEnd();
            JToken personData = JToken.Parse(APIText);
            JToken Data = personData["data"];
            return Data;
        }
    }
}