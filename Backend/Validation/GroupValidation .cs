using Backend.DTO.Request;
using FluentValidation;

namespace Backend.Validation
{
    public class GroupValidatior : AbstractValidator<GroupRequestDTO>
    {
        public GroupValidatior()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class GroupPermissionValidatior : AbstractValidator<GroupPermissionRequestDTO>
    {
        public GroupPermissionValidatior()
        {
            RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.PermissionIds).NotEmpty();
        }
    }

    public class GroupUserValidatior : AbstractValidator<GroupUserRequestDTO>
    {
        public GroupUserValidatior()
        {
            RuleFor(x => x.UserIds).NotEmpty();
            RuleFor(x => x.GroupId).NotEmpty();
        }
    }
}
