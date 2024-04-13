using Backend.Interfaces.Services;
using DAL.Data.Context;
using DAL.Data.Models;

namespace Backend.Services
{
    public class UserService : ServiceBase<User>,IUserService
    {
        public UserService(AppDbContext dbContext):base(dbContext)
        {
        }
    }
}
