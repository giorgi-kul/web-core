using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebCore.Domain.Entities;

namespace WebCore.Domain.Models.Admin
{
    public class PropertyModel
    {
        public PropertyInfo Property { get; set; }
        public ModuleSettingsAttribute Settings { get; set; }
        public List<ModuleEntity> Data { get; set; }
        public bool IsEnumerable => Property.PropertyType.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        public string GetPropertyName()
        {
            if (!String.IsNullOrWhiteSpace(Settings?.DisplayName))
            {
                return Settings?.DisplayName;
            }

            return Property.Name;
        }
    }
}
