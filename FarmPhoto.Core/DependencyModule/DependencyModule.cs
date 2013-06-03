using FarmPhoto.Repository;
using Ninject.Modules;

namespace FarmPhoto.Core.DependencyModule
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhotoRepository>().To<PhotoRepository>();
        }
    }
}
