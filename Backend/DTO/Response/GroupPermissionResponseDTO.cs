namespace Backend.DTO.Response
{
    public class GroupPermissionResponseDTO
    {
        public Guid GroupId { get; set; }
        public PermissionResponseDTO[]? Permissions { get; set; }
    }
}
