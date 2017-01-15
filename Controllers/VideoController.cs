using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multilink2.Controllers
{
    public class VideoController : Controller
    {
        //
        // GET: /Video/

        public ActionResult WeblinkUpdate()
        {
            ViewBag.UserLocation = "Weblink Update Jan 2017";
            ViewBag.BaseLocation = "Video";
            return View();
        }

    }
}
