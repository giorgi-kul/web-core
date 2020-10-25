using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCore.Domain.Database;
using WebCore.Domain.Entities;
using WebCore.Domain.Interfaces;

namespace WebCore.Services
{
    public class UserActionService : IUserActionService
    {
        private readonly DataContext _context;
        public UserActionService(DataContext context)
        {
            _context = context;
        }

        public async Task CreateLoginAction(string location, Administrator administrator)
        {
            await CreateAction(UserActionType.Login, location, administrator);
        }

        private async Task<int> CreateAction(UserActionType type, string location, Administrator administrator)
        {
            AdministratorAction userAction = new AdministratorAction()
            {
                CreateDate = DateTime.Now,
                Type = type,
                Location = location,
                Administrator = administrator,
                CreatedBy = null,
                LastModifiedBy = null,
                LastModifyDate = null,
                IsDeleted = false,
                IsVisible = true
            };

            _context.AdministratorActions.Add(userAction);

            await _context.SaveChangesAsync();

            return userAction.Id;
        }
    }
}
