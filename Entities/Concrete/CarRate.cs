using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CarRate : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int RaterId { get; set; }
        public short Rate { get; set; }
    }
}
