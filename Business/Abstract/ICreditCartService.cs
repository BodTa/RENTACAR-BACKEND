using Core.Utilites.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCartService:IServiceRepository<CreditCart>
    {
        IResult IsEnough(CreditCart creditcart,DateTime RentDate,DateTime ReturnDate);
        IResult Pay(CreditCart creditCart,DateTime RentDate,DateTime ReturnDate);
    }
}
