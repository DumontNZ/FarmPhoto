using System.Web.Mvc;
using FarmPhoto.Common.Resources;

namespace FarmPhoto.Website.Attributes
{
    public class PasswordCompareAttribute : CompareAttribute 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordCompareAttribute"/> class.
        /// </summary>
        /// <param name="otherProperty">The property to compare with the current property.</param>
        public PasswordCompareAttribute(string otherProperty) : base(otherProperty)
        {
            ErrorMessage = ErrorMessages.PasswordsDoNotMatch;
        }
    }
}