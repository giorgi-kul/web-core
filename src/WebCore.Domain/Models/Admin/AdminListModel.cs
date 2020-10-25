using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Entities;

namespace WebCore.Domain.Models.Admin
{
    public class AdminListModel : AdminModelBase
    {
        public bool DetailsEnabled { get; set; }
        public bool CreateEnabled { get; set; }
        public bool EditEnabled { get; set; }
        public bool DeleteEnabled { get; set; }
        public int TotalCount { get; set; }
        public List<PropertyModel> Properties { get; set; }
        public List<ModuleEntity> Items { get; set; }
        public bool SortingEnabled { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
