using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, SqlContext>, ICarDal
    {
        public List<CarDetailsDTO> CarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (SqlContext context = new SqlContext())
            {
                var carDetails = from car in filter is null ? context.Cars : context.Cars.Where(filter)
                                 join brand in context.Brands
                                 on car.BrandId equals brand.BrandId
                                 join color in context.Colors
                                 on car.ColorId equals color.ColorId
                                 select new CarDetailsDTO
                                 {
                                     CarId = car.CarId,
                                     BrandId = brand.BrandId,
                                     ColorId = color.ColorId,
                                     CarName = car.CarName,
                                     BrandName = brand.BrandName,
                                     ColorName = color.ColorName,
                                     FuelType = car.FuelType,
                                     EngineCapacity = car.EngineCapacity,
                                     HorsePower = car.HorsePower,
                                     Kilometer = car.Kilometer,
                                     GearType = car.GearType,
                                     ModelYear = car.ModelYear,
                                     DailyPrice = car.DailyPrice,
                                     Description = car.Description,
                                     DoorCount = car.DoorCount,
                                     SellerId = car.SellerId,
                                    Images= context.Images.Where(i=> i.CarId==car.CarId).ToList(),                                 
                                 };
                return carDetails.ToList();
            }
        }
    }
}
