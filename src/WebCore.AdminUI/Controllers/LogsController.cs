using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebCore.AdminUI.Models;
using WebCore.Domain.Database;
using WebCore.Domain.Entities;
using WebCore.Domain.Interfaces;
using WebCore.Services;

namespace WebCore.AdminUI.Controllers
{
    public class LogsController : GenericController<Log>
    {
        protected override bool DetailsEnabled => true;
        protected override bool CreateEnabled => false;
        protected override bool DeleteEnabled => false;
        protected override bool EditEnabled => false;

        public LogsController(
            DataContext context,
            ILogger<GenericController<Log>> logger) : base(logger, context)
        {

        }
    }
}
