using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Domain.Entities
{
    public class Language : ModuleEntity
    {
        [ModuleSettings(IsHidden = true)]
        public override string Title => Name;

        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Abbreviation { get; set; }
        public string Culture { get; set; }
        public bool IsDefault { get; set; }
    }
}
