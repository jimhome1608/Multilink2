using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multilink2.Models;

namespace Multilink2.Controllers
{
    public class SupportController : Controller
    {
        //
        // GET: /Support/

        public ActionResult vwCheckJobs()
        {
            List<CheckJobs> _list;
            _list = CheckJobs.GetList();
            ViewBag.UserLocation = "Check XML Jobs";
            ViewBag.BaseLocation = "Support";
            return View(_list);
        }

    }
}
