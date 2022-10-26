using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOS
{
   public class CustomerForRegisterDto : IDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string TelNumber { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }

    }
}
