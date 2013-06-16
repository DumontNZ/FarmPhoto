using Ninject.Modules;
using FarmPhoto.Common.Configuration;

namespace FarmPhoto.Repository.DependencyModule
{
    public class DependencyModule : NinjectModule 
    {
        public override void Load()
        {
            Bind<IConfig>().To<Config>(); 
        }
    }
}
