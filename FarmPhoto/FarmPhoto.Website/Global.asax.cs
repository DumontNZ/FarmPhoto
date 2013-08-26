using System;
using Ninject;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;
using FarmPhoto.Website.App_Start;

namespace FarmPhoto.Website
{
    public class MvcApplication : HttpApplication
    {

        [Inject]
        public IKernel Kernel { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void InstantiateKernel(object sender, EventArgs e)
        {
            HttpContext.Current.Items[HttpContextKeys.Kernel] = Kernel;
        }
    }
}