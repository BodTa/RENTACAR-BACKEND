using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOS
{
    public class UserDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public string TelNumber { get; set; }
        public string Email { get; set; }

        public UserPicture UserPicture { get; set; }


    }
}
