using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Domain.Models.Admin;

namespace WebCore.Domain.Entities
{
    public class Log : ModuleEntity
    {
        [ModuleSettings(Filter = true)]
        public string Level { get; set; }
        [ModuleSettings(DisplayName = nameof(Message))]
        public override string Title => Message?.Length > 80 ? $"{Message.Substring(0, 80)}..." : Message;
        [ModuleSettings(IsListVisible = false, Filter = true)]
        public string Message { get; set; }
        [ModuleSettings(Filter = true)]
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
