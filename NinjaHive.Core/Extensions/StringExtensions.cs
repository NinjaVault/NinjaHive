using System.Text.RegularExpressions;

namespace NinjaHive.Core.Extensions
{
    public static class StringExtensions
    {
        //http://stackoverflow.com/questions/6219454/efficient-way-to-remove-all-whitespace-from-string
        public static string RemoveAllWhiteSpace(this string text)
        {
            var fix = Regex.Replace(text, @"\s+", string.Empty);
            return fix.Trim();
        }
    }
}
