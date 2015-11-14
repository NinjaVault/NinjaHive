using System;
using System.Web.Mvc;
using NinjaHive.Contract.Commands;
using NinjaHive.Contract.DTOs;
using NinjaHive.Contract.Queries;
using NinjaHive.Core;
using NinjaHive.WebApp.Services;

namespace NinjaHive.WebApp.Controllers
{
    public class StatsController : Controller
    {
        private StatInfo[] example;

        public StatsController()
        {
            example = new StatInfo[1];
            example[0] = new StatInfo();

            example[0].Id = new Guid();
            example[0].Agility = 0;
            example[0].Defense = 10;
            example[0].Health = -23;
            example[0].Intelligence = 0;
        }

        public ActionResult Index()
        {
            return View(example);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StatInfo stat)
        {
            stat.Id = Guid.NewGuid();
           
            var redirectUri = UrlProvider<StatsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        public ActionResult Edit(Guid statId)
        {
            return View(example[0]);
        }      
    }
}
