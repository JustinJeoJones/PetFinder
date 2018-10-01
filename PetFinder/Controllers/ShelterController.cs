using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PetFinder.Views
{
    public class ShelterController : Controller
    {
        // GET: Shelter
        [HttpGet]
        public ActionResult FindShelter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindShelter(int? zipcode)
        {
            if (zipcode != null)
            {
                return RedirectToAction("ShelterResult",new { zip = zipcode });
            }
            
            return View();
        }

        public ActionResult ShelterResult(int zip)
        {
            HttpWebRequest WR = WebRequest.CreateHttp("http://api.petfinder.com/shelter.find?key=ad29bfe79d7472a7094451439946b3ef&location="+ zip + "&format=json");
            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();
            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string petData = Reader.ReadToEnd();

            JObject JsonData = JObject.Parse(petData);
            ViewBag.ShelterList = JsonData["petfinder"]["shelters"]["shelter"];
            ViewBag.Message = "Page for viewing pet data.";
            return View();
        }

        public ActionResult PetSearch(string id)
        {
            HttpWebRequest WR = WebRequest.CreateHttp("http://api.petfinder.com/shelter.getPets?key=ad29bfe79d7472a7094451439946b3ef&id=" + id+ "&format=json");
            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();
            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string petData = Reader.ReadToEnd();

                JObject JsonData = JObject.Parse(petData);
            ViewBag.PetList = JsonData["petfinder"]["pets"]["pet"];
            WR = WebRequest.CreateHttp("http://api.petfinder.com/shelter.get?key=ad29bfe79d7472a7094451439946b3ef&id="+id+"&format=json");
             Response = (HttpWebResponse)WR.GetResponse();
             Reader = new StreamReader(Response.GetResponseStream());
            petData = Reader.ReadToEnd();

             JsonData = JObject.Parse(petData);
            ViewBag.ShelterInfo = JsonData["petfinder"]["shelter"];
            ViewBag.Message = "Page for viewing pet data.";
            return View();
        }

    }
}