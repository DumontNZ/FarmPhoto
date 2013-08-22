using System.Web.Mvc;
using FarmPhoto.Core;
using Ninject.Modules;
using FarmPhoto.Website.Attributes;
using FarmPhoto.Core.Authentication;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace FarmPhoto.Website.DependencyModule
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhotoManager>().To<PhotoManager>();
            Bind<ITagManager>().To<TagManager>();
            Bind<IUserManager>().To<UserManager>();
            Bind<IRatingManager>().To<RatingManager>();
            Bind<IFormsAuthenticationManager>().To<FormsAuthenticationManager>(); 

            this.BindFilter<FarmPhotoAuthorizeAttribute>(FilterScope.Action, 0).InRequestScope();
        }
    }
}