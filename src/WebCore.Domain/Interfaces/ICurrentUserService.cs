using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Domain.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string Email { get; }
    }
}
