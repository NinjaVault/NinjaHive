using System;
using System.Web.Mvc;
using NinjaHive.Contract.DTOs;
using NinjaHive.WebApp.Services;

namespace NinjaHive.WebApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly GameItem demoGameItem;

        public ItemController()
        {
            this.demoGameItem = new GameItem
            {
                ItemId = Guid.NewGuid(),
                Name = "Demo item"
            };
        }

        public ActionResult Index()
        {
            var items = new[] {this.demoGameItem};

            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid itemId)
        {
            var item = this.demoGameItem;
           
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(GameItem gameItem)
        {
            var redirectUri = UrlProvider<ItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        [HttpPost]
        public ActionResult Create(GameItem gameItem)
        {
            var redirectUri = UrlProvider<ItemController>.GetRouteValues(c => c.Edit(gameItem.ItemId));
            return RedirectToRoute(redirectUri);
        }

        public ActionResult Delete(GameItem gameItem)
        {
            var redirectUri = UrlProvider<ItemController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}