using prjSellor.Dal;
using prjSellor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjSellor.Controllers
{
    public class AppointmentController : Controller
    {
        public ActionResult Index()     //first initialize
        {
            TimeTable TT = new TimeTable();
            return View(TT);
        }


        [Authorize]
        public ActionResult Subscribe(DateTime startDate, int timeSlace)
        {   
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home", null);
            DataLayer dal = new DataLayer();
            Schedule sc = new Schedule();
            sc.startDate = startDate;
            sc.userName = Session["userName"].ToString();
            sc.endDate = startDate.AddMinutes(timeSlace);
            dal.Add(sc);
            Session["Status"] = "Appointment has been created for you!";
            return RedirectToAction("Index");
        }

        public ActionResult Remove(DateTime startDate)
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home", null);
            DataLayer dal = new DataLayer();
            dal.schedule.Where(u => u.startDate == startDate)   //fix this
              .ToList().ForEach(u => dal.schedule.Remove(u));
            dal.SaveChanges();
            Session["Status"] = "The appointment has been canceled";
            return RedirectToAction("Index");

        }

        public ActionResult MyAppointments()
        {
            if (Session["userName"] == null)
                return RedirectToAction("Login", "Home", null);
            svm showViewModel = new svm();
            string userName = Session["userName"].ToString();
            DataLayer dal = new DataLayer();
            showViewModel.scheList = (from u in dal.schedule
                                      where (u.userName == userName && u.startDate > DateTime.Now)
                                      select u).ToList<Schedule>();
            return View(showViewModel);
        }


    }
}