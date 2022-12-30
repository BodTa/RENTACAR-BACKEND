using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOS
{
    public class AddCarWithImagesDTO :IEntity
    {
        public int SellerId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }

        public string CarModel { get; set; }
        public string CarType { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public short DoorCount { get; set; }
        public string FuelType { get; set; }
        public short EngineCapacity { get; set; }
        public short HorsePower { get; set; }
        public int Kilometer { get; set; }
        public string GearType { get; set; }

        public List<IFormFile> files { get; set; }
    }
}
