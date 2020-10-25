using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Domain.Entities
{
    public class Log : ModuleEntity
    {
        public string Level { get; set; }
        public override string Title => Message?.Length > 80 ? $"{Message.Substring(0, 80)}..." : Message;
        [ModuleSettings(IsListVisible = false)]
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public string Exception { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public string Properties { get; set; }

        [ModuleSettings(IsListVisible = false)]
        public override bool IsVisible { get => base.IsVisible; set => base.IsVisible = value; }
        [ModuleSettings(IsListVisible = false)]
        public override DateTime CreateDate { get => base.CreateDate; set => base.CreateDate = value; }
    }
}
