using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RoofTop.Web.App_Start;
using RoofTop.Web.DependencyResolvers;
using System.Net.Mail;

namespace RoofTop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMapping();
            var controllerFactory = new IoCControllerFactory(DependencyConfig.RegisterTypes());
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);


        }

        //Catch unhandled exceptions
        void Application_Error(Object sender, EventArgs e)
        {
            var adminEmail = "james_alcaraz11@yahoo.com";
            var exception = Server.GetLastError();
            if (exception == null)
                return;
            var mail = new MailMessage { From = new MailAddress(adminEmail) };
            mail.To.Add(new MailAddress(adminEmail));
            mail.Subject = "Site Error at " + DateTime.Now;
            mail.Body = "Error Description: " + exception.Message;
            var server = new SmtpClient { Host = "smtp.mail.yahoo.com" };
            server.Send(mail);

            // Clear the error
            Server.ClearError();

            // Redirect to a landing page
            //Response.Redirect("home/landing");
        }
    }
}
