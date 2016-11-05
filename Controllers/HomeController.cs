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

        public ActionResult vwhowLongONREA(String SalesMethod)
        {
            List<howLongONREA> _list;
            if ((Session["howLongONREAData"] == null) || (Session["SalesMethod"] != SalesMethod))
            {
                _list = howLongONREA.GetList(SalesMethod);
                Session["howLongONREAData"] = _list;
            }            
            return View(Session["howLongONREAData"]);
        }


        public ActionResult vwhowLongONREAPartial()
        {           
            return PartialView(Session["howLongONREAData"]);
        }


        public ActionResult vwInternetEnquiries(String SalesMethod)
        {
            List<WebEnquires> _list;
            if ((Session["TypedListModel"] == null) || (Session["SalesMethod"] != SalesMethod))
            {
                _list = WebEnquires.GetList(SalesMethod);
                float _total_count = 0;
                float _rea_count = 0;
                float _dca_count = 0;
                float _rvw_count = 0;
                float _homely_count = 0;
                int _property_count = 0;
                int _distinct_managers = (from _WebEnquires in _list select _WebEnquires.manager).Distinct().Count();
                foreach (WebEnquires _WebEnquires in _list)
                {
                    _total_count = _total_count + _WebEnquires.total_count;
                    _rea_count = _rea_count + _WebEnquires.rea_count;
                    _dca_count = _dca_count + _WebEnquires.dca_count;
                    _rvw_count = _rvw_count + _WebEnquires.rvw_count;
                    _homely_count = _homely_count + _WebEnquires.homely_count;
                    _property_count++;
                }
                ViewBag.PropertiesCaption = "Listings<br /> <span class='percent_used'> " + String.Format(" {0:0} Current", _property_count);
                ViewBag.AgentsCaption = "Agents<br /> <span class='percent_used'> " + String.Format("{0:0} Managing", _distinct_managers);
                ViewBag.EnquiresCaption = "Enquiries<br /> <span class='percent_used'> " + String.Format("Total: {0:0}", _total_count);
                ViewBag.ReaCaption = "REA<br /> <span class='percent_used'> " + String.Format("{0:0.0}%", _rea_count/_total_count*100);
                ViewBag.DcaCaption = "DCA<br /> <span class='percent_used'> " + String.Format("{0:0.0}%", _dca_count / _total_count * 100);
                ViewBag.RvwCaption = "RVW<br /> <span class='percent_used'> " + String.Format("{0:0.0}% </span>", _rvw_count / _total_count * 100);
                ViewBag.HomelyCaption = "Homely<br /> <span class='percent_used'> " + String.Format("{0:0.0}% </span>", _homely_count / _total_count * 100);
                
                Session["TypedListModel"] = _list;
                Session["PropertiesCaption"] = ViewBag.PropertiesCaption;
                Session["AgentsCaption"] = ViewBag.AgentsCaption;
                Session["EnquiresCaption"] = ViewBag.EnquiresCaption;
                Session["ReaCaption"] = ViewBag.ReaCaption;
                Session["DcaCaption"] = ViewBag.DcaCaption;
                Session["RvwCaption"] = ViewBag.RvwCaption;
                Session["HomelyCaption"] = ViewBag.HomelyCaption;
            }
               
            Session["SalesMethod"] = SalesMethod;
            SalesMethod = SalesMethod.Replace("and", "&");
            ViewBag.UserLocation = "Web Enquiries";
            ViewBag.UserLocation2 = SalesMethod;            
            ViewBag.OfficeName = "Noel Jones - Box Hill";
            return View(Session["TypedListModel"]);
        }
        public ActionResult vwInternetEnquiriesPartial()
        {
            ViewBag.PropertiesCaption = Session["PropertiesCaption"];
            ViewBag.AgentsCaption = Session["AgentsCaption"];
            ViewBag.EnquiresCaption = Session["EnquiresCaption"];
            ViewBag.ReaCaption = Session["ReaCaption"];
            ViewBag.DcaCaption = Session["DcaCaption"];
            ViewBag.RvwCaption = Session["RvwCaption"];
            ViewBag.PropertiesCaption = Session["PropertiesCaption"];
            ViewBag.HomelyCaption = Session["HomelyCaption"];
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