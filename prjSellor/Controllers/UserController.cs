using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjSellor.Models;
using prjSellor.Dal;
using prjSellor.Controllers;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace prjSellor.Controllers
{
    public class UserController : Controller
    {

        public ActionResult registerUser() //registeration page
        {
            if (Session["userName"] != null)
                return RedirectToAction("Index", "Appointment", null);
            ViewBag.passwordError = "";
            ViewBag.usernameError = "";
            return View("register", new User());
        }


        public ActionResult registerCheck(User usr) //register user send sql if ok
        {
            if (Session["userName"] != null)
                return RedirectToAction("Index", "Appointment", null);
            if (ModelState.IsValid)
            {
                if (usr.password != Request.Form["passwordConfirm"])
                {
                    ViewBag.passwordError = "password confirmation failed.";
                    return View("register", usr);
                }
                DataLayer dal = new DataLayer();
                if (dal.users.Count(u => u.userName == usr.userName) == 0)  //no username exists
                {
                    usr.type = "Customer";
                    if (usr.hashPass("signUp"))
                    {
                        dal.users.Add(usr);     //check if user exists or not
                        dal.SaveChanges();
                        Session["userName"] = usr.userName;
                        Session["type"] = usr.type;
                        Session["firstName"] = usr.firstName;
                        Session["lastName"] = usr.lastName;
                        Session["phone"] = usr.phone;
                        Session["layOut"] = "~/Views/Shared/layoutUser.cshtml";
                        FormsAuthentication.SetAuthCookie("Customer", true);
                        Session["Status"] = "Registeration complete! welcome and enjoy ";
                        return RedirectToAction("Index", "Appointment", null);
                    }
                    return View("register", usr);
                }
                else
                {
                    ViewBag.usernameError = "Username already exists";
                    return View("register", usr);
                }
            }
            else
                return View("register", usr);       //if js not enabled and modelstat is not valid

        }

        [Authorize]
        public ActionResult myInfo()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Index", "Appointment", null);
            User usr = new User();
            usr.firstName = Session["firstName"].ToString();
            usr.lastName = Session["lastName"].ToString();
            usr.phone = Session["phone"].ToString();
            usr.userName = Session["userName"].ToString();
            usr.password = "default";
            return View(usr);
        }

        [Authorize]
        public ActionResult editCheck(User usr)
        {
            if (Session["userName"] == null)
                return RedirectToAction("Index", "Appointment", null);
            if (ModelState.IsValidField("firstName") && ModelState.IsValidField("lastName") && ModelState.IsValidField("phone"))
            {
                DataLayer dal = new DataLayer();
                usr.userName = Session["userName"].ToString();
                usr.type = "def";
                usr.password = "default";
                dal.users.Attach(usr);
                dal.Entry(usr).Property(x => x.firstName).IsModified = true;
                dal.Entry(usr).Property(x => x.lastName).IsModified = true;
                dal.Entry(usr).Property(x => x.phone).IsModified = true;
                Session["status"] = "Data has been changed";
                dal.SaveChanges();
                Session["firstName"] = usr.firstName;
                Session["lastName"] = usr.lastName;
                Session["phone"] = Request.Form["phone"].ToString();
                return RedirectToAction("Index", "Appointment", null);

            }

            return View("myInfo", usr);
        }

        [Authorize]
        public ActionResult changePassword()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Index", "Appointment", null);
            Session["Status"] = "";
            return View();
        }

        public ActionResult passwordCheck(User usr)
        {
            Session["Status"] = "Passwords dosent match";
            if (ModelState.IsValidField("password") && Request.Form["passwordConfirm"].ToString() == usr.password)
            {
                if(new User(Session["userName"].ToString(), Request.Form["oldPassword"]).checkUsernamePassword() != null)
                {
                    usr.type = "customer";
                    usr.phone = "0";
                    usr.lastName = "def";
                    usr.firstName = "def";
                    usr.userName = Session["userName"].ToString();
                    usr.hashPass("update");
                    DataLayer dal = new DataLayer();
                    dal.users.Attach(usr);
                    dal.Entry(usr).Property(x => x.password).IsModified = true;
                    dal.SaveChanges();
                    Session["Status"] = "Password has been changed";
                    return RedirectToAction("Index", "Appointment", null);
                }
                Session["Status"] = "Old password is wrong.";
            }
            return View("changePassword");
        }


    }
}