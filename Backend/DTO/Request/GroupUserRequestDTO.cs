namespace Backend.DTO.Request
{
    public class GroupUserRequestDTO
    {
        public Guid GroupId { get; set; }
        public Guid[] UserIds { get; set; }
    }
}
