using System;
using FarmPhoto.Website.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FarmPhoto.Website.Models
{
    public class LoginModel
    {
        [Required]
        [LocalisedDisplayName("Username")]
        public String Username { get; set; }

        [Required]
        [LocalisedDisplayName("Password")]
        public String Password { get; set; }

        [LocalisedDisplayName("RememberMe")]
        public bool RememberMe { get; set; }

    }
}