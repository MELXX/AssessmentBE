namespace Backend.DTO.Response
{
    public class GroupUserResponseDTO
    {
        public Guid GroupId { get; set; }
        public UserResponseDTO[] Permissions { get; set; }
    }
}
