
namespace FarmPhoto.Website.Models
{
    public class EmailPasswordResetModel
    {
        public string FirstName { get; set; }
        public string PasswordResetLink { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}