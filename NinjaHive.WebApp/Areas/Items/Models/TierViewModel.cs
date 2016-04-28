using NinjaHive.Contract.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    [ValidateInput(false)]
    public class TierViewModel
    {
        public EquipmentModel Equipment { get; set; }
        public TierModel Tier { get; set; }
    }
}