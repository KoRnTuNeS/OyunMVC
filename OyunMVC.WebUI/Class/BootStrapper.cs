using Autofac;
using Autofac.Integration.Mvc;
using OyunMVC.Core.Infrastructure;
using OyunMVC.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OyunMVC.WebUI.Class
{
    public class BootStrapper
    {
        public static void RunConfig()
        {
            BuildAutoFac();
        }
        private static void BuildAutoFac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RolRepository>().As<IRolRepository>();
            builder.RegisterType<KullaniciRepository>().As<IKullaniciRepository>();
            builder.RegisterType<KategoriRepository>().As<IKategoriRepository>();
            builder.RegisterType<HaberRepository>().As<IHaberRepository>();
            builder.RegisterType<ResimRepository>().As<IResimRepository>();
            builder.RegisterType<EtiketRepository>().As<IEtiketRepository>();
            builder.RegisterType<SliderRepository>().As<ISliderRepository>();
            builder.RegisterType<YorumRespository>().As<IYorumRepository>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}