using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Domain.Entities
{
    public class BaseEntity
    {
        private DateTime? _createDate;

        [ModuleSettings(Order = 1, IsHidden = true)]
        public int Id { get; set; }

        public virtual DateTime CreateDate
        {
            get
            {
                return _createDate.HasValue ? _createDate.Value : (_createDate = DateTime.Now).Value;
            }
            set
            {
                _createDate = value;
            }
        }
    }
}
