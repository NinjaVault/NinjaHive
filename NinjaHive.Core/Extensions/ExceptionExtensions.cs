using System;

namespace NinjaHive.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool IsThrownFrom(this Exception exception, Type type)
        {
            return exception.TargetSite.DeclaringType == type;
        }
    }
}
