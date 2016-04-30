using System;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public interface ITierViewModel
    {
        Guid EquipmentId { get; set; }
        string EquipmentName { get; set; }
    }
}
