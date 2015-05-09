using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using RoofTop.Core.ApplicationServices;
using RoofTop.Core.DomainServices;
using RoofTop.Infrastructure.BLL;
using RoofTop.Infrastructure.BLL.ApplicationServices;
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
            _container.RegisterType<IRealEstateAdService, RealEstateAdService>();
            _container.RegisterType<ICityService, CityService>();
            _container.RegisterType<IRealEstateImageService, RealEstateImageService>();
            _container.RegisterType<IFileService, UserFileService>();
            _container.RegisterType<ICurrentUserService, CurrentUserService>();
            _container.RegisterType<IMappingService, MappingService>( new PerRequestLifetimeManager());
            _container.RegisterInstance<HttpServerUtilityBase>( new HttpServerUtilityWrapper(HttpContext.Current.Server) );
            _container.RegisterType<IIdentity>(new InjectionFactory(u => HttpContext.Current.User.Identity));
            _container.RegisterType<IApplicationDbContext, ApplicationDbContext>( new PerRequestLifetimeManager());

            
            //Unity would pick the constructor with most parameters so we need to tell to pick the parameterless one
            _container.RegisterType<AccountController>(new InjectionConstructor());
            _container.RegisterType<ManageController>(new InjectionConstructor());

            return _container;
        }
    }
}
