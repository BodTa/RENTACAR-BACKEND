using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
   public class Image : IEntity
    {
            public int ImageId { get; set; }
            public int CarId { get; set; }
            public string ImagePath { get; set; }
            public DateTime Date { get; set; }
        public Image()
        {
            Date = DateTime.Now;
        }

    }
}
