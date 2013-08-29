using System;
using FarmPhoto.Common.Extensions;
using FarmPhoto.Domain;

namespace FarmPhoto.Core.Extensions
{
    public static class UserExtensions
    {
        public static string GetEmailQueryStringValue(this User user)
        {
            return "?u={0}&t={1}".FormatWith(user.UserName, user.Token); 
        }

        public static bool HasToken(this User user)
        {
            return user.Token.IsNotNullOrWhiteSpace(); 
        }

        public static bool TokenNotExpired(this User user)
        {
            return user.TokenExpiry.HasValue && user.TokenExpiry >= DateTime.UtcNow;
        }

        public static bool HasValidToken(this User user, string token)
        {
            return user.HasToken() && user.TokenNotExpired() && user.Token == token; 
        }
    }
}
