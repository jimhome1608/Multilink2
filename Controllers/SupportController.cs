using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multilink2.Controllers
{
    public class SupportController : Controller
    {
        //
        // GET: /Support/

        public ActionResult vwFindAgent()
        {
            ViewBag.UserLocation = "Find Agent";
            ViewBag.BaseLocation = "Support";
            return View();
        }

    }
}
