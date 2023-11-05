using FluentValidation;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class CreateUserValidator : AbstractValidator<UserRequest>
    {

        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname is required.");
            RuleFor(x => x.FirstName).MinimumLength(20).WithMessage("Firstname length min value is 20.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Email).MinimumLength(20).WithMessage("Email length min value is 20.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.LastName).MinimumLength(20).WithMessage("LastName length min value is 20.");
        }
    }
}
