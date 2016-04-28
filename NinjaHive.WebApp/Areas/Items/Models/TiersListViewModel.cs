using NinjaHive.Contract.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    [ValidateInput(false)]
    public class TiersListViewModel
    {
        public EquipmentModel Equipment { get; set; }
        public IEnumerable<TierModel> Tiers { get; set; }
    }
}