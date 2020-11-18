using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.AdminUI.Models;
using WebCore.AdminUI.Models.VcModels;
using WebCore.Domain.Entities;
using WebCore.Domain.Models.Admin;

namespace WebCore.AdminUI.ViewComponents
{
    public class GenericFieldCheckboxViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PropertyModel property, ModuleEntity item, PageMode pageMode)
        {
            return Task.FromResult((IViewComponentResult)View("", new GenericFieldVcModel()
            {
                Property = property,
                Item = item,
                PageMode = pageMode
            }));
        }
    }
}
