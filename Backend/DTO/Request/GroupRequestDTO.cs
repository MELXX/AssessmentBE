using Backend.DTO.Response;

namespace Backend.DTO.Request
{
    public class GroupRequestDTO:DtoBase
    {
        public  Guid? Id { get; set; }
        public string Name { get; set; }

    }
}
