using System.Web.Mvc;
using FarmPhoto.Core;
using Ninject.Modules;
using FarmPhoto.Website.Attributes;
using FarmPhoto.Core.Authentication;
using FarmPhoto.Common.Configuration;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace FarmPhoto.Website.DependencyModule
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhotoManager>().To<PhotoManager>();
            Bind<IConfig>().To<Config>();
            Bind<IFormsAuthenticationManager>().To<FormsAuthenticationManager>(); 

            this.BindFilter<FarmPhotoAuthorizeAttribute>(FilterScope.Action, 0).InRequestScope();
        }
    }
}