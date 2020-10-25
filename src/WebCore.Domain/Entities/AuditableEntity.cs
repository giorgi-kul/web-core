using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Domain.Entities
{
    public class AuditableEntity : BaseEntity
    {
        [ModuleSettings(IsListVisible = false)]
        public Administrator CreatedBy { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public Administrator LastModifiedBy { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public DateTime? LastModifyDate { get; set; }
    }
}
