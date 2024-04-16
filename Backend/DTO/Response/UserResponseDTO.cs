namespace Backend.DTO.Response
{
    public class UserResponseDTO: DtoBase
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public UserResponseDTO()
        {

        }

        public UserResponseDTO(Guid id,string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            base.Id = id;
        }

    }
}
