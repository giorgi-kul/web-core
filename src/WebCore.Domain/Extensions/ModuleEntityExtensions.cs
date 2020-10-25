using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCore.Domain.Entities;

namespace WebCore.Domain.Extensions
{
    public static class ModuleEntityExtensions
    {
        public static IQueryable<T> ActiveSet<T>(this DbSet<T> set) where T : ModuleEntity
        {
            return set.Where(i => !i.IsDeleted);
        }

        public static IQueryable<T> ActiveSet<T>(this IQueryable<T> set) where T : ModuleEntity
        {
            return set.Where(i => !i.IsDeleted);
        }

        public static IQueryable<T> VisibleSet<T>(this DbSet<T> set) where T : ModuleEntity
        {
            return set.ActiveSet().Where(i => i.IsVisible);
        }

        public static IQueryable<T> VisibleSet<T>(this IQueryable<T> set) where T : ModuleEntity
        {
            return set.ActiveSet().Where(i => i.IsVisible);
        }
    }
}
