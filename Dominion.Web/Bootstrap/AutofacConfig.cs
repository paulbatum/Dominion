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
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var host = new SolitaireHost();
            host.CreateNewGame("Paul");
            builder.RegisterInstance(host)
                .As<IGameHost>();                

            var provider = new ContainerProvider(builder.Build());
            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(provider));
        }
    }
}