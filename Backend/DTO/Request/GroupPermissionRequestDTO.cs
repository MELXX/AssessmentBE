namespace Backend.DTO.Request
{
    public class GroupPermissionRequestDTO
    {
        public Guid GroupId { get; set; }
        public Guid[] PermissionIds { get; set; }
    }
}
