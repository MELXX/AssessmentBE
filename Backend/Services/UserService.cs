using Backend.Interfaces.Services;
using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserService : ServiceBase<User>,IUserService
    {
        public UserService(AppDbContext dbContext):base(dbContext)
        {
        }

        public async Task<int> Count()
        {
            return await _context.User.CountAsync();
        }
    }
}
