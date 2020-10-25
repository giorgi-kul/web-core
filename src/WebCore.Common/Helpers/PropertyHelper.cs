using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Common.Helpers
{
    public class PropertyHelper
    {
        public static List<PropertyModel> Parse(object item)
        {
            List<PropertyModel> parsedData = new List<PropertyModel>();

            PropertyInfo[] properties = item.GetType().GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                PropertyModel model = new PropertyModel();
                object[] attributes = prop.GetCustomAttributes(false);

                model.Property = prop;

                ModuleSettingsAttribute settings = new ModuleSettingsAttribute();
                foreach (object attr in attributes)
                {
                    settings = attr as ModuleSettingsAttribute ?? new ModuleSettingsAttribute();
                }
                model.Settings = settings;

                parsedData.Add(model);
            }

            foreach (PropertyModel propItem in parsedData)
            {
                if (propItem.Settings.Order == 0)
                {
                    propItem.Settings.Order = parsedData.Max(p => p.Settings.Order) + 1;
                }
            }

            return parsedData.OrderBy(d => d.Settings.Order).ToList();
        }
    }
}
