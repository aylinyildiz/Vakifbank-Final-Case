using FluentValidation;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class CreateEftValidator : AbstractValidator<EftRequest>
    {
        public CreateEftValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.ReceiverName).NotEmpty();
            RuleFor(x => x.ReceiverAddress).NotEmpty();
            RuleFor(x => x.ReceiverAddressType).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.ChargeAmount).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.TransactionCode).NotEmpty();
            RuleFor(x => x.TransactionDate).NotEmpty();
        }
    }
}
