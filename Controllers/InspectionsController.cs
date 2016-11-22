using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multilink2.Models;
using System.IO;
using Newtonsoft.Json;

namespace Multilink2.Controllers
{
    public class InspectionsController : Controller
    {
        //
        // GET: /Inspections/

        public ActionResult ViewInspections()
        {
            InspectionList inspectionList = new InspectionList();
            inspectionList.loadFromDB(); 
            ViewBag.UserLocation = "Inspection";
            ViewBag.UserLocation2 = "Sat. 19/11/2016";
            ViewBag.BaseLocation = "8-18 Glass St";
            return View(inspectionList);
        }

        [HttpPost]
        public string saveInspections(String SalesMethod)
        {
            Stream stream = Request.InputStream;
            StreamReader reader = new StreamReader(stream, Request.ContentEncoding);
            string text = reader.ReadToEnd();
            InspectionList _inspectionList = JsonConvert.DeserializeObject<InspectionList>(text);
            return _inspectionList.writeToDB();
        }

    }
}
