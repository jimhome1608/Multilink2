using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multilink2.Controllers
{
    public class InspectionsController : Controller
    {
        //
        // GET: /Inspections/

        public ActionResult Inspections()
        {
            ViewBag.UserLocation = "Inspection";
            ViewBag.UserLocation2 = "Sat. 19/11/2016";
            ViewBag.BaseLocation = "8-18 Glass St";
            return View();
        }

    }
}
