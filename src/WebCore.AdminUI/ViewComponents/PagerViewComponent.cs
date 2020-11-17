using Microsoft.AspNetCore.Http.Extensions;
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
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(int shownPageNumber, int totalItems, int itemsPerPage)
        {
            int pageCount = Convert.ToInt32(Math.Ceiling((double)totalItems / (double)itemsPerPage));
            string currentUrl = Request.GetDisplayUrl();

            int queryStringIndex = currentUrl.IndexOf("?");
            if (queryStringIndex > 0)
                currentUrl = currentUrl.Substring(0, queryStringIndex);

            bool showLeftArrow = true;
            bool showRigthArrow = true;
            int currentPageNumber = 1;
            System.Text.StringBuilder queryStrings = new System.Text.StringBuilder();

            foreach (String key in Request.Query.Keys)
                if (key != "page")
                    queryStrings.Append($"&{key}={Request.Query[key]}");

            if (Request.Query.Keys.Contains("page"))
            {
                if (int.TryParse(Request.Query["page"], out currentPageNumber))
                {
                    if (currentPageNumber > pageCount)
                        currentPageNumber = pageCount;

                    if (currentPageNumber <= 0)
                        currentPageNumber = 1;
                }
                else
                    currentPageNumber = 1;
            }

            string previousUrl = currentPageNumber != 1 ? $"{currentUrl}?page={currentPageNumber - 1}{queryStrings}" : "javascript:void(0)";
            string nextUrl = currentPageNumber != pageCount ? $"{currentUrl}?page={currentPageNumber + 1}{queryStrings}" : "javascript:void(0)";
            string firstPageUrl = $"{currentUrl}?page=1{queryStrings}";
            string firstPageActive = currentPageNumber == 1 ? "active" : "";
            string lastPageUrl = $"{currentUrl}?page={pageCount}{queryStrings}";

            string navStyle = string.Empty;

            if (totalItems <= 0)
                navStyle = "display:none;";

            return Task.FromResult((IViewComponentResult)View("", new PagerVcModel()
            {
                NavStyle = navStyle,
                ShowLeftArrow = showLeftArrow,
                ShowRightArrow = showRigthArrow,
                CurrentPageNumber = currentPageNumber,
                CurrentUrl = currentUrl,
                PageCount = pageCount,
                ShownPageNumber = shownPageNumber,
                QueryString = queryStrings.ToString(),
                NextUrl = nextUrl,
                PreviousUrl = previousUrl
            }));
        }
    }
}
