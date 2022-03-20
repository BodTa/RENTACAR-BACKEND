using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
   public class CreditCart:IEntity
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string CardNumber { get; set; }
        public int CCV { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
