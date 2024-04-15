using Backend.DTO.Request;
using FluentValidation;

namespace Backend.Validation
{
    public class PermissionValidatior : AbstractValidator<PermissionRequestDTO>
    {
        public PermissionValidatior()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
