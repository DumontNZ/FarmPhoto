using FarmPhoto.Website.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FarmPhoto.Website.Models
{
    public class PasswordResetModel
    {
        public string Username { get; set; }

        public bool HasValidToken { get; set; }

        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [LocalisedDisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [LocalisedDisplayName("ReenterPassword")]
        [PasswordCompare("Password")]
        public string ReenterPassword { get; set; }
    }
}