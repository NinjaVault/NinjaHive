using NinjaHive.Contract.Models;
using System;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    [ValidateInput(false)]
    public class TierViewModel : ITierViewModel
    {
        public TierViewModel(TierModel tier, Guid equipmentId, string equipmentName = "")
        {
            Tier = tier;
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
        }
        public TierViewModel(TierModel tier, EquipmentModel equipment) :
            this(tier, equipment.Id, equipment.Name)
        {
        }

        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public TierModel Tier { get; set; }
    }
}