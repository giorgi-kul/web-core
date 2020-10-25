using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Domain.Entities
{
    public class ModuleEntity : AuditableEntity
    {
        public virtual string Title { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public virtual int OrderIndex { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public virtual bool IsDeleted { get; set; }
        public virtual bool IsVisible { get; set; }
    }
}
