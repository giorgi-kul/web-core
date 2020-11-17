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
    public class GenericFieldInputViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PropertyModel property, ModuleEntity item, bool? readOnly)
        {
            return Task.FromResult((IViewComponentResult)View("", new GenericFieldInputVcModel()
            {
                Property = property,
                Item = item,
                ReadOnly = readOnly
            }));
        }
    }
}
