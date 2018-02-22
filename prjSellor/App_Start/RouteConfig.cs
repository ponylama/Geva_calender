using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace prjSellor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {


            routes.MapRoute(
            name: "MyAppointments",
            url: "MyAppointments",
            defaults: new { controller = "Appointment", action = "MyAppointments", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Myinfo",
            url: "Myinfo",
            defaults: new { controller = "User", action = "Myinfo", id = UrlParameter.Optional }
            );
            

            routes.MapRoute(
            name: "Logout",
            url: "logout",
            defaults: new { controller = "Home", action = "logOut", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Contact",
            url: "Contact",
            defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "About",
            url: "About",
            defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Register",
            url: "Register",
            defaults: new { controller = "User", action = "registerUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Login",
            url: "Login",
            defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );



            routes.MapRoute(
            name: "DefaultPage",
            url: "",
            defaults: new { controller = "Appointment", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Home",
            url: "Home",
            defaults: new { controller = "Appointment", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Admin",
            url: "Admin",
           defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Customers",
            url: "Customers",
            defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
