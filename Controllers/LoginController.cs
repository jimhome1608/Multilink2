using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Multilink2.Models;
using Multilink2.Helpers;


namespace Multilink2.Controllers
{
    public class LoginController : Controller
    {

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel loginModel = new LoginModel();
            ViewBag.UserLocation = "Login";
            ViewBag.BaseLocation = "Welcome";
            ViewBag.UserName = "";
            if (Request.Cookies["multilink"] != null)
            {
                if (Request.Cookies["multilink"]["UserName"] != null)
                    loginModel.UserName = Request.Cookies["multilink"]["UserName"];
            }
            return View(loginModel);
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.login())
                {
                    Session["multilink_login"] = "ok";
                    Session["user_full_name"] = model.full_name;
                    Session["user_office_id"] = model.user_office_id;
                    if (model.user_office_id == 1)
                        Session["show_support_menu"] = "yes";
                    else
                        Session["show_support_menu"] = "no";

                    Session["user_photo"] = model.user_photo;                    
                    HttpCookie myCookie = new HttpCookie("multilink");
                    myCookie["UserName"] = model.UserName;
                    myCookie.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(myCookie);
                    ViewBag.user_full_name = Session["user_full_name"];
                    return Redirect(returnUrl ?? "~/Home/vwhowLongONREA/Sales");
                }
                ViewBag.ErrorMessage = model.login_result;
            }
            Session["multilink_login"] = "Nok";
            Session["show_support_menu"] = "no";
            ViewBag.UserLocation = "Login";
            ViewBag.BaseLocation = "Welcome";
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            Session["multilink_login"] = "Nok";
            Session["show_support_menu"] = "no";
            return Redirect("~/login/login");
        }

    }
}
