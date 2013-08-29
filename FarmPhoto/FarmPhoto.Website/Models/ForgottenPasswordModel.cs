using System.ComponentModel.DataAnnotations;
using FarmPhoto.Website.Attributes;

namespace FarmPhoto.Website.Models
{
    public class ForgottenPasswordModel
    {
        [Required]
        [LocalisedDisplayName("Username")]
        public string Username { get; set; }
    }
}