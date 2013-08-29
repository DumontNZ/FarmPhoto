using System;

namespace FarmPhoto.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public DateTime CreatedOnDateUTC { get; set; }
        public DateTime DeletedOnDateUTC { get; set; }
        public string PasswordSalt { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiry { get; set; }
        public bool Administrator { get; set; }
    }
}
