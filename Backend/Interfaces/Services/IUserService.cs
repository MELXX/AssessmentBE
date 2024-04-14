using DAL.Data.Models;

namespace Backend.Interfaces.Services
{
    public interface IUserService:ICRUDServiceBase<User>
    {
        Task<int> Count();
    }
}
