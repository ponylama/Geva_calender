using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjSellor.Dal;
using prjSellor.Models;
using System.Threading;

namespace prjSellor.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult showUsers()
        {
            if (Session["type"] != null)
            {
                if (Session["type"].ToString() == "Admin")
                {
                    return View();
                }
            }
            Session["status"] = "";
            return RedirectToAction("Index", "Appointment", null);
        }

        public ActionResult getUsersByJSON()
        {
            if (Session["type"] != null)
            {
                if (Session["type"].ToString() == "Admin")
                {
                    DataLayer dal = new DataLayer();
                    List<User> objUsers = dal.users.ToList<User>();
                    return Json(objUsers, JsonRequestBehavior.AllowGet);
                }
            }
            Session["status"] = "";
            return RedirectToAction("Index", "Appointment", null);
        }


        public ActionResult changeUserAccess()
        {
            if (Session["type"] != null)
            {
                if (Session["type"].ToString() == "Admin")
                {
                    return View(new searchUser());
                }
            }
            Session["status"] = "";
            return RedirectToAction("Index", "Appointment", null);
        }

        [HttpPost]
        public ActionResult SearchINDB()
        {
            if (Session["type"] != null)
            {
                if (Session["type"].ToString() == "Admin")
                {
                    return View("changeUserAccess", new searchUser(Request.Form["stringSearch"]));
                }
            }
            Session["status"] = "";
            return RedirectToAction("Index", "Appointment", null);
        }

        public ActionResult changeAccess(string userName, string type)
        {
            if (Session["type"] != null)
            {
                if (Session["type"].ToString() == "Admin")
                {
                    User usr = new User();
                    DataLayer dal = new DataLayer();
                    usr.userName = userName;
                    usr.password = "default";
                    usr.firstName = "def";
                    usr.lastName = "def";
                    usr.phone = "0";
                    if (type == "Admin")
                    {
                        usr.type = "Customer";
                    }
                    else if (type == "Customer")
                    {
                        usr.type = "Admin";
                    }
                    dal.users.Attach(usr);
                    dal.Entry(usr).Property(x => x.type).IsModified = true;
                    dal.SaveChanges();
                    Session["status"] = "Data has been changed";
                    return View("changeUserAccess", new searchUser());
                }
            }
            Session["status"] = "";
            return RedirectToAction("Index", "Appointment", null);
        }

    }
}