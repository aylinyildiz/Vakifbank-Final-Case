using FluentValidation;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class CreateAddressValidator : AbstractValidator<AddressRequest>
    {
        public CreateAddressValidator()
        {
            RuleFor(x => x.AddressLine1).NotEmpty().MinimumLength(10).MaximumLength(50);
            RuleFor(x => x.AddressLine2).NotEmpty().MinimumLength(10).MaximumLength(50);
            RuleFor(x => x.UserId).NotEqual(0).NotEmpty();
            RuleFor(x => x.County).NotEmpty().MinimumLength(10).MaximumLength(50);
            RuleFor(x => x.City).NotEmpty().MinimumLength(10).MaximumLength(50);
            RuleFor(x => x.PostalCode).NotEmpty().MinimumLength(4).MaximumLength(10);
        }
    }
}
