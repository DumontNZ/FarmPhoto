using System.ComponentModel;
using FarmPhoto.Common.Resources;

namespace FarmPhoto.Website.Attributes
{
    public class LocalisedDisplayNameAttribute : DisplayNameAttribute 
    {
        private readonly string _resourceName;

        public LocalisedDisplayNameAttribute(string resourceName)
        {
            _resourceName = resourceName;
        }

        public override string DisplayName
        {
            get { return DisplayNames.ResourceManager.GetString(_resourceName); }
        }
    }
}