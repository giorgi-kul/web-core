using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WebCore.Domain.Models.Admin.Requests
{
    public class GetGenericListDataRequest<T>
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public Expression<Func<T, bool>> FilterPredicate { get; set; }
        public Expression<Func<T, object>> OrderPredicate { get; set; }
    }
}
