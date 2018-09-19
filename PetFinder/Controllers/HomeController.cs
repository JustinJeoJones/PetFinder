using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PetFinder.Controllers
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

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PetView()
        {
            HttpWebRequest WR = WebRequest.CreateHttp("http://api.petfinder.com/pet.get?key=ad29bfe79d7472a7094451439946b3ef&id=42739495&format=json");
            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();
            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string petData = Reader.ReadToEnd();

            JObject JsonData = JObject.Parse(petData);
            ViewBag.pet = JsonData["petfinder"]["pet"]["name"]["$t"];
            ViewBag.Image = JsonData["petfinder"]["pet"]["media"]["photos"]["photo"][3]["$t"];
            ViewBag.Message = "Page for viewing pet data.";

            return View();
        }
    }
}