using NinjaHive.Contract.Models;
using NinjaHive.WebApp.Areas.Items.Controllers;
using NinjaHive.WebApp.Extensions;
using NinjaHive.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    [ValidateInput(false)]
    public class TiersListViewModel: ITierViewModel
    {
        public TiersListViewModel(IEnumerable<TierModel> tiers, Guid equipmentId, string equipmentName = "")
        {
            Tiers = tiers;
            EquipmentId = equipmentId;
            EquipmentName = equipmentName;
        }
        public TiersListViewModel(IEnumerable<TierModel> tiers, EquipmentModel equipment) :
            this(tiers, equipment.Id, equipment.Name)
        {
        }

        public Guid EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        
        public IEnumerable<TierModel> Tiers { get; set; }
    }
}