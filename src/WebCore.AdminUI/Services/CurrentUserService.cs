using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WebCore.Domain.Interfaces;

namespace WebCore.AdminUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public int? UserId { get; }
        public string Email { get; }
    }
}
