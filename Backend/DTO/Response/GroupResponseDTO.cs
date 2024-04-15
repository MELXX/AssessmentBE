namespace Backend.DTO.Response
{
    public class GroupResponseDTO: DtoBase
    {
        public string Name { get; set; }
        public ICollection<UserResponseDTO>? Users { get; set; }
        public ICollection<PermissionResponseDTO>? Permissions { get; set; }

    }
}
