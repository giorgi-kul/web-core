using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Domain.Entities
{
    public class BaseEntity
    {
        private DateTime? _createDate;

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
