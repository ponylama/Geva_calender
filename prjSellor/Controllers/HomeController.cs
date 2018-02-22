using prjSellor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjSellor.Dal;
using System.Web.Security;
using System.Web.Configuration;

namespace prjSellor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Appointment", null);
        }

        public ActionResult Login()
        {
            if (Session["userName"] == null)
            {
                Session["Status"] = "";
                return View();
            }
            else
                return RedirectToAction("Index", "Appointment", null);
        }


        public ActionResult SubmitLogin()
        {
            if (Request.Form["userName"] != "" && Request.Form["password"] != "")
            {
                User connectedUser = new User(Request.Form["userName"], Request.Form["password"]).checkUsernamePassword();
                if (connectedUser != null)
                {
                    Session["userName"] = connectedUser.userName;
                    Session["type"] = connectedUser.type;
                    Session["firstName"] = connectedUser.firstName;
                    Session["lastName"] = connectedUser.lastName;
                    Session["phone"] = connectedUser.phone;
                    if (connectedUser.type == "Admin")
                        Session["layOut"] = "~/Views/Shared/layoutAdmin.cshtml";
                    else
                        Session["layOut"] = "~/Views/Shared/layoutUser.cshtml";
                    Session["Status"] = "Hello and welcome back " + connectedUser.firstName;
                    return RedirectToAction("Index", "Appointment", null);
                }
            }

            Session["Status"] = "Username or password incorrect.";
            return View("Login");

        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult logOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["Status"] = "You have been successfully logged out";
            return RedirectToAction("Index", "Appointment", null);
        }
    }
}