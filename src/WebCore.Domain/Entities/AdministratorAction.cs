using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Domain.Entities
{
    public class AdministratorAction
    {
        public int Id { get; set; }
        public Administrator Administrator { get; set; }
        public DateTime Date { get; set; }
        public UserActionType Type { get; set; }
        public string Location { get; set; }
    }


    public enum UserActionType
    {
        None = 0,
        Login = 1,
        Create = 2,
        Edit = 3,
        Delete = 4
    }
}
