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
    public class GenericRenderPropertiesViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(List<PropertyModel> properties, ModuleEntity item, PageMode pageMode)
        {
            return Task.FromResult((IViewComponentResult)View("", new GenericRenderPropertiesVcModel()
            {
                Properties = properties,
                Item = item,
                PageMode = pageMode
            }));
        }
    }
}
