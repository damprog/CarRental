using CarRental.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(b => b.PricePerDay)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Car>()
                .Property(b => b.PricePerHour)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Rental>()
                .Property(b => b.RentPrice)
                .HasPrecision(18, 2);

        }

    }
}
