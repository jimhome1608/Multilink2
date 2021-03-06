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

        public ActionResult ViewOnMap(String SalesMethod)
        {
            ViewBag.UserLocation = "View on Map";
            ViewBag.BaseLocation = "Current Listings";
            Session["SalesMethod"] = SalesMethod;
            SalesMethod = SalesMethod.Replace("and", "&");
            ViewBag.UserLocation2 = SalesMethod;
            int _office_id = 0;
            if (Session["user_office_id"] != null)
                _office_id = (int)Session["user_office_id"];
            String _oids = "?oids[]=" + _office_id.ToString();
            if (_office_id == 91)
                _oids = _oids + "&oids[]=92";
            if (_office_id == 92)
                _oids = _oids + "&oids[]=91";
            // http://www.multilink.com.au/multilinkmap/index.php/?oids[]=91&oids[]=92&mode=rent
            ViewBag.MapURL = "http://www.multilink.com.au/multilinkmap/index.php/"+ _oids + "&mode=sales";
            if (SalesMethod == "Rentals")
                ViewBag.MapURL = "http://www.multilink.com.au/multilinkmap/index.php/" + _oids + "&mode=rent";
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult vwhowLongONREA(String SalesMethod)
        {
            List<howLongONREA> _list;
            if ((Session["howLongONREAData"] == null) || (Session["SalesMethod"] != SalesMethod))
            {
                int _rea_this_month = 0;
                int _rea_last_month = 0;
                int _office_id = 0;
                if (Session["user_office_id"] != null)
                    _office_id = (int)Session["user_office_id"];

                string _current_month = String.Format("{0:MMMM}", DateTime.Now);
                string _last_month = String.Format("{0:MMMM}", DateTime.Now.AddMonths(-1));
                _list = howLongONREA.GetList(SalesMethod, _office_id);
                _rea_this_month = _list.Where(s => (s.rea_start.Month == DateTime.Now.Month && s.rea_start.Year == DateTime.Now.Year)).OrderBy(s => s.rea_start).Count();
                _rea_last_month = _list.Where(s => (s.rea_start.Month == DateTime.Now.Month-1 && s.rea_start.Year == DateTime.Now.Year)).OrderBy(s => s.rea_start).Count();
                Session["howLongONREAData"] = _list;
                ViewBag.captionhowLongONREAData = "REA<br /><span class='percent_used'>" + _current_month + String.Format(" +{0}", _rea_this_month) + "</span>"+
                                                  "<br /><span class='percent_used2'>" + _last_month + String.Format(" +{0}", _rea_last_month) + "</span>"; 

                Session["captionhowLongONREAData"] = ViewBag.captionhowLongONREAData;
            }
            ViewBag.user_full_name = Session["user_full_name"];
            ViewBag.UserLocation = "Internet Status";            
            ViewBag.BaseLocation = "Current Listings";
            Session["SalesMethod"] = SalesMethod;
            SalesMethod = SalesMethod.Replace("and", "&");
            ViewBag.UserLocation2 = SalesMethod;
            return View(Session["howLongONREAData"]);
        }


        public ActionResult vwhowLongONREAPartial()
        {
            ViewBag.user_full_name = Session["user_full_name"];
            ViewBag.captionhowLongONREAData = Session["captionhowLongONREAData"];
            return PartialView(Session["howLongONREAData"]);
        }


        public ActionResult vwInternetEnquiries(String SalesMethod)
        {
            List<WebEnquires> _list;
            if ((Session["TypedListModel"] == null) || (Session["SalesMethod"] != SalesMethod))
            {
                int _office_id = 0;
                if (Session["user_office_id"] != null)
                    _office_id = (int)Session["user_office_id"];
                ViewBag.user_full_name = Session["user_full_name"];
                _list = WebEnquires.GetList(_office_id,SalesMethod);
                float _total_count = 0;
                float _inspection_count = 0;
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
                    _inspection_count = _inspection_count + _WebEnquires.inspections;
                    _property_count++;
                }
                ViewBag.PropertiesCaption = "Listings<br /> <span class='percent_used'> " + String.Format(" {0:0} Current", _property_count);
                ViewBag.AgentsCaption = "Agents<br /> <span class='percent_used'> " + String.Format("{0:0} Managing", _distinct_managers);
                ViewBag.EnquiresCaption = "Enquiries<br /> <span class='percent_used'> " + String.Format("Total: {0:0}", _total_count);
                ViewBag.InspectionsCaption = "Inspections<br /> <span class='percent_used'> " + String.Format("Total: {0:0}", _inspection_count);
                ViewBag.ReaCaption = "REA<br /> <span class='percent_used'> " + String.Format("{0:0.0}%", _rea_count/_total_count*100);
                ViewBag.DcaCaption = "DCA<br /> <span class='percent_used'> " + String.Format("{0:0.0}%", _dca_count / _total_count * 100);
                ViewBag.RvwCaption = "RVW<br /> <span class='percent_used'> " + String.Format("{0:0.0}% </span>", _rvw_count / _total_count * 100);
                ViewBag.HomelyCaption = "Homely<br /> <span class='percent_used'> " + String.Format("{0:0.0}% </span>", _homely_count / _total_count * 100);
                
                Session["TypedListModel"] = _list;
                Session["PropertiesCaption"] = ViewBag.PropertiesCaption;
                Session["AgentsCaption"] = ViewBag.AgentsCaption;              
                Session["EnquiresCaption"] = ViewBag.EnquiresCaption;
                Session["InspectionsCaption"] = ViewBag.InspectionsCaption;
                Session["ReaCaption"] = ViewBag.ReaCaption;
                Session["DcaCaption"] = ViewBag.DcaCaption;
                Session["RvwCaption"] = ViewBag.RvwCaption;
                Session["HomelyCaption"] = ViewBag.HomelyCaption;
            }
               
            Session["SalesMethod"] = SalesMethod;
            SalesMethod = SalesMethod.Replace("and", "&");
            ViewBag.UserLocation = "Inspections &amp; Enquiries";
            ViewBag.UserLocation2 = SalesMethod;            
            ViewBag.BaseLocation = "Current Listings";
            return View(Session["TypedListModel"]);
        }
        public ActionResult vwInternetEnquiriesPartial()
        {
            ViewBag.PropertiesCaption = Session["PropertiesCaption"];
            ViewBag.AgentsCaption = Session["AgentsCaption"];
            ViewBag.EnquiresCaption = Session["EnquiresCaption"];
            ViewBag.InspectionsCaption = Session["InspectionsCaption"];
            ViewBag.ReaCaption = Session["ReaCaption"];
            ViewBag.DcaCaption = Session["DcaCaption"];
            ViewBag.RvwCaption = Session["RvwCaption"];
            ViewBag.PropertiesCaption = Session["PropertiesCaption"];
            ViewBag.HomelyCaption = Session["HomelyCaption"];
            ViewBag.user_full_name = Session["user_full_name"];
            return PartialView(Session["TypedListModel"]);
        }

        public ActionResult vwHomePage()
        {
            ViewBag.UserLocation = "Inspections";
            ViewBag.BaseLocation = "Current Listings";
            return View();
        }

        public ActionResult vwAllActivity()
        {
            ViewBag.UserLocation = "Inspections &amp; Enquiries";
            ViewBag.BaseLocation = "Current Listingsl";
            return View("vwHomePage");
        }

        public ActionResult Video()
        {
            ViewBag.UserLocation = "Intro. Video";
            ViewBag.BaseLocation = "Noel Jones - Box Hill";
            return View();
        }



    }
}