using FarmPhoto.Common;
using FarmPhoto.Common.Cryptography;
using FarmPhoto.Repository;
using Ninject.Modules;

namespace FarmPhoto.Core.DependencyModule
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhotoRepository>().To<PhotoRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ICryptography>().To<Cryptography>();
        
            Bind<ITagRepository>().To<TagRepository>();
        }
    }
}
