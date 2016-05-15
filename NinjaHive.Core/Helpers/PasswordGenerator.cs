using System;
using System.Text;

namespace NinjaHive.Core.Helpers
{
    public static class PasswordGenerator
    {
        public static string GeneratePassword()
        {
            var allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var builder = new StringBuilder();
            
            for(var i = 0; i < 8; i++)
            {
                var j = random.Next(allowedChars.Length);
                var nextChar = allowedChars[j];
                builder.Append(nextChar);
            }

            return builder.ToString();
        }
    }
}
