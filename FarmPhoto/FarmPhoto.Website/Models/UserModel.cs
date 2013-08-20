using System.ComponentModel.DataAnnotations;
using FarmPhoto.Website.Attributes;

namespace FarmPhoto.Website.Models
{
    public class UserModel
    {
        [Required]
        [LocalisedDisplayName("Username")]
        public string Username { get; set; }

        // [EmailAddress]
        [Required]
        [LocalisedDisplayName("EmailAddress")]
        public string EmailAddress { get; set; }

        [Required]
        [LocalisedDisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [LocalisedDisplayName("Surname")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [LocalisedDisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [LocalisedDisplayName("ReenterPassword")]
        [PasswordCompare("Password")]
        public string ReenterPassword { get; set; }

        [LocalisedDisplayName("Country")]
        public string Country { get; set; }

    }
}