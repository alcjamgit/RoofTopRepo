using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RoofTop.Web.App_Start;
using RoofTop.Web.DependencyResolvers;

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
    }
}
