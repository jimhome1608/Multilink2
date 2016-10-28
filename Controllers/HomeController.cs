using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multilink2.Models;

namespace Multilink2.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult vwInternetEnquiries()
        {
            if (Session["TypedListModel"] == null)
                Session["TypedListModel"] = WebEnquires.GetList();

            return View(Session["TypedListModel"]);
        }
        public ActionResult vwInternetEnquiriesPartial()
        {
            return PartialView(Session["TypedListModel"]);
        }
    }
}