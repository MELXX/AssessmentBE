namespace Backend.DTO.Response
{
    public class GroupPermissionResponseDTO
    {
        public Guid GroupId { get; set; }
        public ICollection<PermissionResponseDTO>? Permissions { get; set; } = new PermissionResponseDTO[0];
    }
}
