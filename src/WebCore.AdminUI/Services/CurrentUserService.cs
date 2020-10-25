using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using WebCore.Domain.Interfaces;

namespace WebCore.AdminUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal user = httpContextAccessor.HttpContext?.User;

            if (user != null)
            {
                Email = user.FindFirstValue(ClaimTypes.NameIdentifier);
                UserId = user.FindFirstValue(ClaimTypes.Sid);
            }
        }

        public string UserId { get; }
        public string Email { get; }
    }
}
