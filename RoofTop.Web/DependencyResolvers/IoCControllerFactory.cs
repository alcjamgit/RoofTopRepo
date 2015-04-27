using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace RoofTop.Web.DependencyResolvers
{
    public class IoCControllerFactory: DefaultControllerFactory
    {
        private readonly IUnityContainer _container;
        public IoCControllerFactory(IUnityContainer container)
        {
            _container = container;
        }
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, 
            Type controllerType)
        {
            if (controllerType != null)
            {
                return _container.Resolve(controllerType) as IController;
            }
            else
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
        }

    }
}
