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
        private readonly ILogger<GenericController<T>> _logger;
        private readonly DataContext _context;
        private PageMode _currentPageMode = PageMode.None;


        protected virtual string ListView => "~/Views/Generic/List.cshtml";
        protected virtual string CreateEditDetailsView => "~/Views/Generic/AddEdit.cshtml";
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
            ILogger<GenericController<T>> logger,
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
        public async Task<IActionResult> Create()
        {
            if (!CreateEnabled)
            {
                SetErrorAlert("Create mode is not enabled for this module.");
                return RedirectToList();
            }

            try
            {
                _currentPageMode = PageMode.Create;

                AdminAddEditModel model = new AdminAddEditModel
                {
                    PageMode = _currentPageMode,
                    Properties = Properties,
                    Item = new T()
                };

                return View(CreateEditDetailsView, model);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to load create view");
                return RedirectToList();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(T item, string button)
        {
            if (!CreateEnabled)
            {
                SetErrorAlert("Create mode is not enabled for this module.");
                return RedirectToList();
            }

            try
            {
                item.CreateDate = DateTime.Now;
                item.OrderIndex = _context.Set<T>().ActiveSet().Count() > 0 ? _context.Set<T>().ActiveSet().Max(i => i.OrderIndex) + 1 : 1;

                _context.Set<T>().Add(item);
                await _context.SaveChangesAsync();

                if (button == "save")
                    return RedirectToList();
                else
                    return await RedirectToEdit(item.Id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Failed to create new item ({nameof(T)})", item, button);
                return View(CreateEditDetailsView, new AdminAddEditModel
                {
                    PageMode = _currentPageMode,
                    Properties = Properties,
                    Item = item
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromQuery] int? page, int id)
        {
            if (!DetailsEnabled)
            {
                SetErrorAlert("Details view is not enabled for this module.");
                return RedirectToList(page);
            }

            try
            {
                AdminAddEditModel model = new AdminAddEditModel();

                T item = await _context.Set<T>().ActiveSet().FirstOrDefaultAsync(i => i.Id == id);
                if (item == null)
                {
                    SetErrorAlert("Requested record was not found.");
                    return RedirectToList(page);
                }

                model.PageMode = PageMode.Details;
                model.Properties = Properties;
                model.Item = item;

                return View(CreateEditDetailsView, model);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to load details", page, id);
                return RedirectToList();
            }
        }

        protected virtual Expression<Func<T, bool>> GetFilterPredicate()
        {
            Expression<Func<T, bool>> predicate = x => true;

            foreach (var prop in Properties)
            {
                if (prop.Settings.Filter)
                {
                    string val = Request.Query[$"filter.{prop.Property.Name.ToLower()}"];
                    string valFrom = Request.Query[$"filter.{prop.Property.Name.ToLower()}.from"];
                    string valTo = Request.Query[$"filter.{prop.Property.Name.ToLower()}.to"];

                    if (!String.IsNullOrWhiteSpace(val)
                        || !String.IsNullOrWhiteSpace(valFrom)
                        || !String.IsNullOrWhiteSpace(valTo))
                    {
                        if (prop.Property.PropertyType == typeof(DateTime))
                            predicate = predicate.And(DateTimeFieldFilterPredicate(prop, valFrom, valTo));
                        else
                            predicate = predicate.And(StringFieldFilterPredicate(prop, val));
                    }
                }
            }

            return predicate;
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
        protected virtual Task<IActionResult> RedirectToEdit(int itemId)
        {
            return Task.FromResult((IActionResult)RedirectToAction("edit", new { id = itemId, pageId = Request.Query["pageId"] }));
        }

        private Expression<Func<T, bool>> StringFieldFilterPredicate(PropertyModel prop, string val)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, prop.Property.Name); //x.{DynamicPropertyName}
            var contains = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var value = Expression.Constant(val);
            var body = Expression.Call(property, contains, value); //x.{DynamicPropertyName}.Contains(val)
            return Expression.Lambda<Func<T, bool>>(body, parameter); //x => it.{DynamicPropertyName}.Contains(val)
        }
        private Expression<Func<T, bool>> DateTimeFieldFilterPredicate(PropertyModel prop, string valFrom, string valTo)
        {
            BinaryExpression firstBody = null;
            BinaryExpression secondBody = null;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, prop.Property.Name); //x.{DynamicPropertyName}

            if (!String.IsNullOrWhiteSpace(valFrom))
                if (DateTime.TryParseExact(valFrom, new string[] { "dd.MM.yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime dateFrom))
                    firstBody = Expression.GreaterThanOrEqual(property, Expression.Constant(dateFrom));

            if (!String.IsNullOrWhiteSpace(valTo))
                if (DateTime.TryParseExact(valTo, new string[] { "dd.MM.yyyy" }, null, System.Globalization.DateTimeStyles.None, out DateTime dateTo))
                    secondBody = Expression.LessThanOrEqual(property, Expression.Constant(dateTo.AddDays(1)));

            Expression body = null;
            if (firstBody != null & secondBody != null)
                body = Expression.And(firstBody, secondBody);
            else if (firstBody != null)
                body = firstBody;
            else if (secondBody != null)
                body = secondBody;

            return Expression.Lambda<Func<T, bool>>(body, parameter);
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
                List<PropertyModel> properties = PropertyHelper.Parse(new T());

                if (_currentPageMode == PageMode.Create)
                    foreach (var property in properties)
                        if (property.Property.Name == nameof(AuditableEntity.CreatedBy)
                         || property.Property.Name == nameof(AuditableEntity.LastModifiedBy)
                         || property.Property.Name == nameof(AuditableEntity.LastModifyDate)
                         || property.Property.Name == nameof(AuditableEntity.CreateDate))
                            property.Settings.IsHidden = true;

                return properties;
            }
        }
    }
}
