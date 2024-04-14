using Backend.DTO.Request;
using FluentValidation;

namespace Backend.Validation
{
    public class UserValidatior : AbstractValidator<UserRequestDTO>
    {
        public UserValidatior()
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
