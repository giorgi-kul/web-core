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
        [ModuleSettings(CheckDuplicate = true)]
        public string Name { get; set; }
        [ModuleSettings(CheckDuplicate = true)]
        public string ShortName { get; set; }
        [ModuleSettings(CheckDuplicate = true)]
        public string Abbreviation { get; set; }
        [ModuleSettings(CheckDuplicate = true)]
        public string Culture { get; set; }
        public bool IsDefault { get; set; }
    }
}
