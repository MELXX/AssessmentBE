using Backend.DTO.Response;
using DAL.Data.Models;
using System.Security.Cryptography.X509Certificates;

namespace Backend.Extentions
{
    public static class GroupDTOConverter
    {
        public static PermissionResponseDTO ToPermissionResponseDTO(this Permission permission)
        {
            if (permission == null)
                return new PermissionResponseDTO();
            else
                return new PermissionResponseDTO(permission.Id, permission.Name);
        }

        public static UserResponseDTO ToUserResponseDTO(this User user)
        {
            if (user == null)
                return new UserResponseDTO();
            else
                return new UserResponseDTO(user.Id, user.Name, user.Surname, user.Email);
        }
    }
}
