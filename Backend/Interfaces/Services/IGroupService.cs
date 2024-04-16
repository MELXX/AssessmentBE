using DAL.Data.Models;

namespace Backend.Interfaces.Services
{
    public interface IGroupService:ICRUDServiceBase<Group>
    {
        Task<bool> AddUsersToGroup(Guid GroupId, Guid[] UserIds);
        Task<bool> AddPermissionsToGroup(Guid GroupId, Guid[] PermissionIds);
        Task<Group[]?> GetGroupUsersAndPermission(Guid id = default);
    }
}
