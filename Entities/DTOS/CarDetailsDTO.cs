using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOS
{
    public class CarDetailsDTO : IDTO
    {

        public string BrandName { get; set; }
        public int SellerId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string CarName { get; set; }
        public int CarId { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public int ModelYear { get; set; }
        public List<Image> Images { get; set; }
        public int DoorCount { get; set; }

    }
}
