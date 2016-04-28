using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using NinjaHive.Contract.Models;

namespace NinjaHive.WebApp.Models
{
    [ValidateInput(false)]
    public class EquipmentViewModel
    {
        public EquipmentModel EquipmentItem { get; set; }

        public IEnumerable<TierModel> tiers;

        public IEnumerable<SelectListItem> Tiers
        {
            get { return new SelectList(this.tiers ?? new List<TierModel>(), "Id", "Name"); }
        }
    }
}