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
    public class LanguagesController : GenericController<Language>
    {
        public LanguagesController(
            DataContext context,
            ILogger<GenericController<Language>> logger) : base(logger, context)
        {

        }
    }
}
