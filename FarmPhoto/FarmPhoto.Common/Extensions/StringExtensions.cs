
using System.Text;

namespace FarmPhoto.Common.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string instance, params object[] pareters)
        {
            return string.Format(instance, pareters); 
        }

        public static bool IsNotNullOrWhiteSpace(this string instance)
        {
            return !instance.IsNullOrWhiteSpace();
        }

        public static bool IsNullOrWhiteSpace(this string instance)
        {
            return string.IsNullOrWhiteSpace(instance); 
        }

        public static string RemoveSpecialCharacters(this string instance)
        {
            var stringBuilder = new StringBuilder();
            foreach (char character in instance)
            {
                if ((character >= '0' && character <= '9') || (character >= 'A' && character <= 'Z') || (character >= 'a' && character <= 'z'))
                {
                    stringBuilder.Append(character);
                }
            }
            return stringBuilder.ToString();
        }
    }
}
