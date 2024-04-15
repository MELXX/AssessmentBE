namespace Backend.DTO.Response
{
    public class GroupUserResponseDTO
    {
        public Guid GroupId { get; set; }
        public ICollection<UserResponseDTO>? Permissions { get; set; } = new List<UserResponseDTO>();
    }
}
