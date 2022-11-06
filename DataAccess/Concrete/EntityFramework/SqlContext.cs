using Core.Entities.Concrete;
using Core.Utilites.Security.Jwt;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
   public class SqlContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-2FMD4AR\MSSQLSERVER01;Database=RentCarProject;Trusted_Connection=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            //modelBuilder.Entity<RefreshToken>().HasNoKey();
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<CarComment> CarComments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<UserPicture> UserPictures { get; set; }

        public DbSet<UserRate> UserRates { get; set; }
        public DbSet<CarRate> CarRates { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
