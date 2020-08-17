using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCore.Domain.Entities;

namespace WebCore.Domain.Database
{
    public static class DataContextSeed
    {
        public static async Task SeedAsync(UserManager<Administrator> userManager)
        {
            //var defaultUser = new Administrator { UserName = "giorgi", Email = "kulijanishvilig@gmail.com" };

            //if (userManager.Users.All(u => u.Id != defaultUser.Id))
            //{
            //    await userManager.CreateAsync(defaultUser, "1234");
            //}
        }
    }
}
