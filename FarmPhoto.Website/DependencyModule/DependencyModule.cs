using FarmPhoto.Core;
using Ninject.Modules;

namespace FarmPhoto.Website.DependencyModule
{
    public class DependencyModule : NinjectModule 
    {
        public override void Load()
        {
            Bind<IPhotoManager>().To<PhotoManager>();
        }
    }
}