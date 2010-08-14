using System;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using System.Reflection;
using Dominion.GameHost;

namespace Dominion.Web.Bootstrap
{
    public class AutofacConfig
    {
        private static IContainer _container;


        public static IContainer Container
        {
            get { return _container; }
        }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());            

            var host = new MultiGameHost();            
            builder.RegisterInstance(host)
                .As<MultiGameHost>();

            _container = builder.Build();
            
            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(new ContainerProvider(_container)));
        }
    }    
}