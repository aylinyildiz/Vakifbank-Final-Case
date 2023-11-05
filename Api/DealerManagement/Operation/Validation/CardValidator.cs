using FluentValidation;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class CreateCardValidator : AbstractValidator<CardRequest>
    {
        public CreateCardValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.CardHolder).NotEmpty();
            RuleFor(x => x.CardNumber).NotEmpty();
            RuleFor(x => x.Cvv).NotEmpty();
            RuleFor(x => x.ExpiryDate).NotEmpty();
            RuleFor(x => x.ExpenseLimit).NotEmpty();
        }
    }
}
