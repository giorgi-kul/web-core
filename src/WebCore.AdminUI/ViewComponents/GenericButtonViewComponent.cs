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
    public class GenericButtonViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string action, string controller, string buttonClass, string iconClass, string buttonText, int? id = null)
        {
            string url = $"{Url.Action(action, controller, new { id })}{Request.QueryString.Value}";

            return Task.FromResult((IViewComponentResult)View("", new GenericButtonVcModel()
            {
                Url = url,
                ButtonClass = buttonClass,
                IconClass = iconClass,
                ButtonText = buttonText,
                ItemId = id
            }));
        }
    }
}
