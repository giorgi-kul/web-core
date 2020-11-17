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
        [ModuleSettings(DisplayName = nameof(Message), IsHidden = true)]
        public override string Title => Message?.Length > 80 ? $"{Message.Substring(0, 80)}..." : Message;
        [ModuleSettings(IsListVisible = false, Filter = true)]
        public string Message { get; set; }
        [ModuleSettings(Filter = true)]
        public DateTime TimeStamp { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public string Exception { get; set; }
        [ModuleSettings(IsListVisible = false)]
        public string Properties { get; set; }

        [ModuleSettings(IsListVisible = false, IsHidden = true)]
        public override bool IsVisible { get => base.IsVisible; set => base.IsVisible = value; }
        [ModuleSettings(IsListVisible = false, IsHidden = true)]
        public override DateTime CreateDate { get => base.CreateDate; set => base.CreateDate = value; }
        [ModuleSettings(IsListVisible = false, IsHidden = true)]
        public override bool IsDeleted { get => base.IsDeleted; set => base.IsDeleted = value; }
        [ModuleSettings(IsHidden = true, IsListVisible = false)]
        public override Administrator LastModifiedBy { get => base.LastModifiedBy; set => base.LastModifiedBy = value; }
        [ModuleSettings(IsHidden = true, IsListVisible = false)]
        public override DateTime? LastModifyDate { get => base.LastModifyDate; set => base.LastModifyDate = value; }
        [ModuleSettings(IsHidden = true, IsListVisible = false)]
        public override Administrator CreatedBy { get => base.CreatedBy; set => base.CreatedBy = value; }
        [ModuleSettings(IsHidden = true, IsListVisible = false)]
        public override int OrderIndex { get => base.OrderIndex; set => base.OrderIndex = value; }
    }
}
