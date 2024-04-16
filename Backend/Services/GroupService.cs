using Backend.Interfaces.Services;
using DAL.Data.Context;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class GroupService : ServiceBase<Group>, IGroupService
    {
        public GroupService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddPermissionsToGroup(Guid GroupId, Guid[] PermissionIds)
        {
            var group = await _context.Group.FindAsync(GroupId);

            var permissions = await _context.Permission
                .Where(Permission => PermissionIds.Contains(Permission.Id))
                .ToArrayAsync();

            if (group != default && permissions != default)
            {
                var groupPerm = await _context.GroupPermissions
                 .AsNoTracking()
                .Include(x => x.Group)
                .Include(x => x.Permission)
                .Where(groupPermission => groupPermission.Group.Id == GroupId)
                .ToArrayAsync();
                var activepermission = groupPerm.Select(x => x.Permission.Id);
                var delta = GetDelta(activepermission,PermissionIds);
                if (delta.Any())
                {
                    var toAdd = delta.Select(x => new GroupPermission()
                    {
                        Group = group,
                        Permission = permissions.FirstOrDefault(p => p.Id == x)
                    });

                    await _context.GroupPermissions.AddRangeAsync(toAdd);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> AddUsersToGroup(Guid GroupId, Guid[] UserIds)
        {
            var group = await _context.Group.FindAsync(GroupId);
            var users = await _context.User
                .Where(user => UserIds.Contains(user.Id))
                .ToArrayAsync();
            if (group != default && users != default)
            {
                var groupPerm = await _context.UserGroup
                 .AsNoTracking()
                .Include(x => x.Group)
                .Include(x => x.User)
                .Where(groupUser => groupUser.Group.Id == GroupId)
                .ToArrayAsync();
                var activeUser = groupPerm.Select(x => x.User.Id);
                var delta = GetDelta(activeUser, UserIds);
                if (delta.Any())
                {
                    var toAdd = delta.Select(x => new UserGroup()
                    {
                        Group = group,
                        User = users.FirstOrDefault(u => u.Id == x)
                    });

                    await _context.UserGroup.AddRangeAsync(toAdd);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
        
        public async Task<Group[]?> GetGroupUsersAndPermission(Guid id = default)
        {
            IQueryable<Group> query = _context.Group
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Include(x => x.Permissions)
                .ThenInclude(x => x.Permission);

            if (id != default)
                query = query.Where(x=> x.Id == id);
            return await query.ToArrayAsync();
        }

        private IEnumerable<Guid> GetDelta(IEnumerable<Guid> current, IEnumerable<Guid> newData)
        {
            return newData.Except(current);
        }
    }
}
