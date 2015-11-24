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
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<EditStatCommand> editStatCommandHandler;
        private readonly ICommandHandler<DeleteStatCommand> deleteStatCommand;

        public StatsController(
            IQueryProcessor queryProcessor,
            ICommandHandler<EditStatCommand> editStatCommandHandler,
            ICommandHandler<DeleteStatCommand> deleteStatCommand)
        {
            this.queryProcessor = queryProcessor;
            this.editStatCommandHandler = editStatCommandHandler;
            this.deleteStatCommand = deleteStatCommand;
        }

        public ActionResult Index()
        {
            var stats = this.queryProcessor.Execute(new GetAllStatsQuery());
            return View(stats);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StatInfo stat)
        {
            stat.Id = Guid.NewGuid();
            var command = new EditStatCommand(stat, createNew: true);
            this.editStatCommandHandler.Handle(command);

            var redirectUri = UrlProvider<StatsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }

        public ActionResult Edit(Guid statId)
        {
            var stat = this.queryProcessor.Execute(new GetEntityByIdQuery<StatInfo>(statId));

            return View(stat);
        }

        public ActionResult Delete(StatInfo stat)
        {
            var command = new DeleteStatCommand()
            {
                Stat = stat
            };

            deleteStatCommand.Handle(command);

            var redirectUri = UrlProvider<StatsController>.GetRouteValues(c => c.Index());
            return RedirectToRoute(redirectUri);
        }
    }
}
