using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Domain.Models.Admin
{
    public enum EditorType
    {
        None = 0,
        Textbox = 1,
        Multiline = 2,
        ShortEditor = 3,
        FullEditor = 4
    }
    public class ModuleSettingsAttribute : Attribute
    {
        public bool IsMultilang { get; set; }
        public string DisplayName { get; set; }
        public int Order { get; set; }
        public bool IsListVisible { get; set; } = true;
        public bool Filter { get; set; }
        public bool IsHidden { get; set; }
        public bool IsReadOnly { get; set; }
        public Type RelatedModuleType { get; set; }
        public EditorType EditorType { get; set; }
        public bool IsRelatedModuleMultilang { get; set; }
        public string Hint { get; set; }
        public bool CheckDuplicate { get; set; }
    }
}
