using System.Collections.ObjectModel;
using NinjaHive.Contract;
using NinjaHive.Core;

namespace NinjaHive.WebApp
{
    public interface IUserContextWithRoles : IUserContext
    {
        ReadOnlyCollection<Role> UserRoles { get; }
    }
}
