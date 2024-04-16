namespace Backend.DTO.Response
{
    public class PermissionResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public PermissionResponseDTO()
        {
                
        }

        public PermissionResponseDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
