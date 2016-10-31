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

        public ActionResult vwInternetEnquiries(String SalesMethod)
        {
            if ((Session["TypedListModel"] == null) || (Session["SalesMethod"] != SalesMethod))
               Session["TypedListModel"] = WebEnquires.GetList(SalesMethod);
            Session["SalesMethod"] = SalesMethod;
            SalesMethod = SalesMethod.Replace("and", "&");
            ViewBag.UserLocation = "Web Enquiries";
            ViewBag.UserLocation2 = SalesMethod;
            ViewBag.OfficeName = "Noel Jones - Box Hill";
            return View(Session["TypedListModel"]);
        }
        public ActionResult vwInternetEnquiriesPartial()
        {
            return PartialView(Session["TypedListModel"]);
        }

        public ActionResult vwHomePage()
        {
            ViewBag.UserLocation = "Inspections";
            ViewBag.OfficeName = "Noel Jones - Box Hill";
            return View();
        }

        public ActionResult vwAllActivity()
        {
            ViewBag.UserLocation = "Enquiries and Inspections";
            ViewBag.OfficeName = "Noel Jones - Box Hill";
            return View("vwHomePage");
        }

        public ActionResult Video()
        {
            ViewBag.UserLocation = "Intro. Video";
            ViewBag.OfficeName = "Noel Jones - Box Hill";
            return View();
        }



    }
}