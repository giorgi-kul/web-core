using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Entities;
using WebCore.Domain.Models.Admin;

namespace WebCore.AdminUI.Models.VcModels
{
    public class GenericRenderPropertiesVcModel
    {
        public List<PropertyModel> Properties { get; set; }
        public ModuleEntity Item { get; set; }
        public PageMode PageMode { get; set; }
    }
}
