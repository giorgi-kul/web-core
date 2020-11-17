using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Entities;
using WebCore.Domain.Models.Admin;

namespace WebCore.AdminUI.Models.VcModels
{
    public class GenericFieldInputVcModel
    {
        public PropertyModel Property { get; set; }
        public ModuleEntity Item { get; set; }
        public bool? ReadOnly { get; set; }

        public string Value => this.Property?.Property?.GetValue(this.Item)?.ToString();
    }
}
