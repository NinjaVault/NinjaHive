using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace NinjaHive.Core.Extensions
{
    public static class FriendlyExtensions
    {
        public static string ToFriendlyString(this string text)
        {
            if (text.All(char.IsUpper)) return text;

            var capitalLetterMatch = new Regex("\\B[A-Z]", RegexOptions.Compiled);
            return capitalLetterMatch.Replace(text, " $&");
        }

        public static string ToFriendlyString(this Enum enumType)
        {
            return enumType.ToString().ToFriendlyString();
        }
    }
}
