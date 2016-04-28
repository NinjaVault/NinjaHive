using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Models
{
    [ValidateInput(false)]
    public class TierViewModel
    {
        public TierModel TierItem { get; set; }
    }
}