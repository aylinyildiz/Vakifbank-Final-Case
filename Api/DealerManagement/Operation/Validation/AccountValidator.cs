using FluentValidation;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class CreateAccountValidator : AbstractValidator<AccountRequest>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Name).MinimumLength(20).WithMessage("Name length min value is 20.");
        }
    }
}
