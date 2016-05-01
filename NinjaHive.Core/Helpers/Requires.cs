using System;

namespace NinjaHive.Core.Helpers
{
    public static class Requires
    {
        public static void IsNotNull(this object instance, string parameterName)
        {
            if(instance == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
