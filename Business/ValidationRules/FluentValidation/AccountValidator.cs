using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AccountValidator:AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(a => a.AccountNumber).MinimumLength(5);
           
        }

        
    }
}
