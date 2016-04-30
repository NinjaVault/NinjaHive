using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public interface ITierViewModel
    {
        Guid EquipmentId { get; set; }
        string EquipmentName { get; set; }
    }
}
