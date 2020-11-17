using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Entities;

namespace WebCore.Domain.Models.Admin
{
    public class AdminAddEditModel : AdminModelBase
    {
        public List<PropertyModel> Properties { get; set; }
        public ModuleEntity Item { get; set; }
    }
}
