using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarRateValidator:AbstractValidator<CarRate>
    {
        public CarRateValidator()
        {
            RuleFor(r => (int)r.Rate).GreaterThanOrEqualTo(0);
            RuleFor(r => (int)r.Rate).LessThanOrEqualTo(10); 
        }
    }
}
