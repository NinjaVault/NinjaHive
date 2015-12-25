using System.Web;
using Microsoft.AspNet.Identity;
using NinjaHive.Core;

namespace NinjaHive.WebApp.Services
{
    public class HttpWebUserContext : IUserContext
    {
        public string Id
        {
            get { return HttpContext.Current.User.Identity.GetUserId(); }
        }

        public string UserName
        {
            get { return HttpContext.Current.User.Identity.GetUserName(); }
        }
    }
}