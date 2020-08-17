using System.Threading.Tasks;
using WebCore.Domain.Entities;

namespace WebCore.Domain.Interfaces
{
    public interface IUserActionService
    {
        Task CreateLoginAction(string location, Administrator administrator);
    }
}
