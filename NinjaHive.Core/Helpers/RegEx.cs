namespace NinjaHive.Core.Helpers
{
    public static class RegEx
    {
        public const string AlphaNum = @"([a-zA-Z0-9]+)";
        public const string Alpha = @"([a-zA-Z]+)";
        public const string Num = @"([0-9]+)";
        public const string AlphaNumUnderscoreSpace = @"([a-zA-Z0-9_\s]+)";
        public const string AlphaNumSpace = @"([a-zA-Z0-9\s]+)";
    }
}