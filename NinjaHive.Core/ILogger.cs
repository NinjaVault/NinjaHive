using System;

namespace NinjaHive.Core
{
    public interface ILogger
    {
        void Log(Exception exception);
    }
}
