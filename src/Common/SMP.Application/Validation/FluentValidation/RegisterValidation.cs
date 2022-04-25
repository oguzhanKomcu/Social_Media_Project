using FluentValidation;
using SMP.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Validation.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Enter a email address")
                .EmailAddress()
                .WithMessage("Enter a valid a email address");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Enter a password");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Enter a password")
                .Equal(x => x.Password)
                .WithMessage("Password don't match");
            RuleFor(x => x.UserName)
               .NotEmpty()
               .WithMessage("Enter a user name")
               .MinimumLength(3)
               .MaximumLength(50)
               .WithMessage("Minimum 3, maximum 50 character");


        }

    }
}
