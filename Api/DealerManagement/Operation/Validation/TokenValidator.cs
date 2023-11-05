using FluentValidation;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class CreateTokenValidator : AbstractValidator<TokenRequest>
    {
        public CreateTokenValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithMessage("Password is required.");
        }
    }
}
