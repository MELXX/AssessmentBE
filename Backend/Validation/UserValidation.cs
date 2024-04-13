﻿using Backend.DTO.Request;
using FluentValidation;

namespace Backend.Validation
{
    public class UserValidatior : AbstractValidator<UserRequestDTO>
    {
        public UserValidatior()
        {
            RuleFor(x => x.Surname).NotEmpty();
            //RuleFor(x => x.Forename).NotEmpty().WithMessage("Please specify a first name");
            //RuleFor(x => x.Discount).NotEqual(0).When(x => x.HasDiscount);
            //RuleFor(x => x.Address).Length(20, 250);
            //RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        }

        //private bool BeAValidPostcode(string postcode)
        //{
        //    // custom postcode validating logic goes here
        //}
    }
}
