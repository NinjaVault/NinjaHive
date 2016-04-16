using System.Collections.Generic;
using NinjaHive.Contract;
using NinjaHive.Core;

namespace NinjaHive.WebApp
{
    public interface IUserContextWithRoles : IUserContext
    {
        IReadOnlyCollection<Role> UserRoles { get; }
    }
}
