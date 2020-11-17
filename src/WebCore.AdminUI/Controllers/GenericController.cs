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
        protected virtual string DetailsView => "~/Views/Generic/Details.cshtml";

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

        [HttpGet]
        public async Task<IActionResult> Details([FromQuery] int? page, int id)
        {
            AdminAddEditModel model = new AdminAddEditModel();

            if (!DetailsEnabled)
            {
                SetErrorAlert("Details view is not enabled for this module.");
                return RedirectToList(page);
            }

            T item = await _context.Set<T>().ActiveSet().FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                SetErrorAlert("Requested record was not found.");
                return RedirectToList(page);
            }

            model.Properties = Properties;
            model.Item = item;

            return View(DetailsView, model);
        }

        protected virtual Expression<Func<T, bool>> GetFilterPredicate()
        {
            return x => true;
        }

        protected virtual IQueryable<T> Order(IQueryable<T> query)
        {
            return query.OrderByDescending(t => t.Id);
        }


        protected virtual IActionResult RedirectToList(int? pageId = null)
        {
            return RedirectToAction("", new { page = pageId });
        }
        protected void SetErrorAlert(string message)
        {
            SetAlert(message, AlertType.Error);
        }
        protected void SetSuccessAlert(string message)
        {
            SetAlert(message, AlertType.Success);
        }
        protected void SetWarningAlert(string message)
        {
            SetAlert(message, AlertType.Warning);
        }
        private void SetAlert(string message, AlertType type)
        {
            if (string.IsNullOrWhiteSpace(message)) { return; }

            TempData["alertType"] = type;
            TempData["alertMessage"] = message;
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
