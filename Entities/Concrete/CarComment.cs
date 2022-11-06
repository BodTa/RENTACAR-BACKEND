using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CarComment:IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CommenterId { get; set; }

        public int ParentId { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public CarComment()
        {
            CreatedAt = DateTime.Now;
        }

    }
}
