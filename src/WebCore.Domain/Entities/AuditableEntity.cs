using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Domain.Entities
{
    public class AuditableEntity : BaseEntity
    {
        public Administrator CreatedBy { get; set; }
        public Administrator LastModifiedBy { get; set; }
        public DateTime? LastModifyDate { get; set; }
    }
}
