using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.AdminUI.Models;
using WebCore.AdminUI.Models.VcModels;
using WebCore.Domain.Models.Admin;

namespace WebCore.AdminUI.ViewComponents
{
    public class GenericFilterViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(List<PropertyModel> properties)
        {
            return Task.FromResult((IViewComponentResult)View("", new GenericFilterVcModel()
            {
                Properties = properties
            }));
        }
    }
}
