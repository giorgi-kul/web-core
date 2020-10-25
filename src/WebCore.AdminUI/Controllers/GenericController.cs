using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebCore.AdminUI.Models;
using WebCore.Common.Helpers;
using WebCore.Domain.Database;
using WebCore.Domain.Entities;
using WebCore.Domain.Extensions;
using WebCore.Domain.Interfaces;
using WebCore.Domain.Models.Admin;
using WebCore.Domain.Models.Admin.Requests;

namespace WebCore.AdminUI.Controllers
{
    public abstract class GenericController<T> : BaseAuthController where T : ModuleEntity, new()
    {
        private readonly ILogger<T> _logger;
        private readonly DataContext _context;

        protected virtual string ListView => "~/Views/Generic/List.cshtml";
        protected virtual string CreateView => "~/Views/Generic/AddEdit.cshtml";
        protected virtual string EditView => "~/Views/Generic/AddEdit.cshtml";

        protected virtual bool IsSingleItemView => false;
        protected virtual bool DetailsEnabled => false;
        protected virtual bool CreateEnabled => true;
        protected virtual bool DeleteEnabled => true;
        protected virtual bool EditEnabled => true;
        protected virtual bool SortingEnabled => false;
        protected virtual int ItemsPerPage => 15;
        protected virtual Expression<Func<T, object>> OrderPredicate => x => x.Id;

        public GenericController(
            ILogger<T> logger,
            DataContext context)
        {
            _context = context;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? page)
        {
            if (IsSingleItemView)
            {
                throw new NotImplementedException();
            }
            else
            {
                int currentPage = page ?? 1;

                IQueryable<T> itemsQuery = _context.Set<T>().ActiveSet().OrderByDescending(i => i.Id);

                int totalCount = await itemsQuery.CountAsync();

                itemsQuery = Order(itemsQuery.Where(GetFilterPredicate()));

                if (SortingEnabled)
                {
                    throw new NotImplementedException();
                    //itemsQuery = itemsQuery.OrderByDescending(OrderPredicate);
                }

                List<T> listItems = await itemsQuery.Skip((currentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToListAsync();

                AdminListModel model = CreateAdminListModel(totalCount, listItems);

                //await LoadRelatedData(model.Properties)

                return View(ListView, model);
            }
        }

        protected virtual Expression<Func<T, bool>> GetFilterPredicate()
        {
            return x => true;
        }

        protected virtual IQueryable<T> Order(IQueryable<T> query)
        {
            return query.OrderByDescending(t => t.Id);
        }

        private AdminListModel CreateAdminListModel(int totalCount, List<T> listItems)
        {
            AdminListModel model = new AdminListModel();

            model.TotalCount = totalCount;
            model.Properties = Properties;
            model.Items = listItems.Select(x => x as ModuleEntity).ToList();
            model.CreateEnabled = CreateEnabled;
            model.DeleteEnabled = DeleteEnabled;
            model.DetailsEnabled = DetailsEnabled;
            model.EditEnabled = EditEnabled;
            model.SortingEnabled = SortingEnabled;
            model.ItemsPerPage = ItemsPerPage;

            return model;
        }
        private List<PropertyModel> Properties
        {
            get
            {
                return PropertyHelper.Parse(new T());
            }
        }
    }
}
