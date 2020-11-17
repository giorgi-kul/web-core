using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Domain.Entities
{
    public class AuditableEntity : BaseEntity
    {
        [ModuleSettings(IsListVisible = false)]
        public virtual Administrator CreatedBy { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public virtual Administrator LastModifiedBy { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public virtual DateTime? LastModifyDate { get; set; }
    }
}
