using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserRate: IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RaterId { get; set; }

        public short Rate { get; set; }

    }
}
