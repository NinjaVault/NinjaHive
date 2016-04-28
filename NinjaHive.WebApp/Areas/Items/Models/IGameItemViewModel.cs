using NinjaHive.Contract.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NinjaHive.WebApp.Areas.Items.Models
{
    public interface IGameItemViewModel
    {
        GameItemModel BaseGameItem { get; }

        IEnumerable<SelectListItem> MainCategories { get; }
        IEnumerable<SelectListItem> Categories { get; }
    }
}
