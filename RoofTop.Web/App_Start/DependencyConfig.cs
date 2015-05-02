using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.Unity;
using RoofTop.Core.DomainServices;
using RoofTop.Infrastructure.BLL;
using RoofTop.Infrastructure.BLL.DomainServices;
using RoofTop.Infrastructure.DAL;
using RoofTop.Web.Controllers;

namespace RoofTop.Web
{
    public class DependencyConfig
    {
        private static IUnityContainer _container;
        public static IUnityContainer RegisterTypes()
        { 
            _container = new UnityContainer();
            _container.RegisterType<IRealEstateAdService, RealtEstateAdService>();
            _container.RegisterType<ICityService, CityService>();
            _container.RegisterType<IRealEstateImageService, RealEstateImageService>();
            _container.RegisterInstance<HttpServerUtilityBase>( new HttpServerUtilityWrapper(HttpContext.Current.Server) );
            _container.RegisterType<IApplicationDbContext, ApplicationDbContext>();

            
            //Unity would pick the constructor with most parameters so we need to tell to pick the parameterless one
            _container.RegisterType<AccountController>(new InjectionConstructor());
            _container.RegisterType<ManageController>(new InjectionConstructor());

            return _container;
        }
    }
}
