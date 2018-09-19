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
        string shelterid = "0";
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
            HttpWebRequest WR = WebRequest.CreateHttp("http://api.petfinder.com/breed.list?key=ad29bfe79d7472a7094451439946b3ef&id=42739495&animal=cat&format=json");
            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();
            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string petData = Reader.ReadToEnd();

            JObject JsonData = JObject.Parse(petData);
            ViewBag.BreedList = JsonData["petfinder"]["breeds"]["breed"];
            ViewBag.Message = "Page for viewing pet data.";
            shelterid = id;
            return View();
            
            
        }

    }
}