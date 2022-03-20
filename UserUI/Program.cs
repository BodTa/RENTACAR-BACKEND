using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace UserUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.CarDetails();
            foreach (var Car in result.Data)
            {
                Console.WriteLine(Car.BrandName+ "  "+Car.BrandName+"  "+Car.ColorName +"  "+Car.DailyPrice);             
            }
        }
    }
}
