using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.AdminUI.Models.VcModels
{
    public class PagerVcModel
    {
        public string NavStyle { get; set; }
        public bool ShowLeftArrow { get; set; }
        public bool ShowRightArrow { get; set; }
        public int CurrentPageNumber { get; set; }
        public string CurrentUrl { get; set; }
        public int PageCount { get; set; }
        public int ShownPageNumber { get; set; }
        public string QueryString { get; set; }
        public string NextUrl { get; set; }
        public string PreviousUrl { get; set; }

        public string GetUrl(int pageNumber)
        {
            return $"{this.CurrentUrl}?page={pageNumber}{this.QueryString}";
        }

        public string GetActiveClass(int pageNumber)
        {
            return pageNumber == this.CurrentPageNumber ? "active" : "";
        }
    }
}
