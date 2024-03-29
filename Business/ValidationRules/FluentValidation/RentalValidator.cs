﻿using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
         public RentalValidator()
        {
            RuleFor(r => r.RentDate).GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(r => r.ReturnDate).GreaterThanOrEqualTo(DateTime.Now.AddDays(5));
        }
    }
}
