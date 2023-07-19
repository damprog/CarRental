using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Domain
{
    public class Rental : Entity
    {
        [Required]
        public Guid CarId { get; protected set; }
        [Required]
        [MaxLength(500)]
        public string CarName { get; protected set; }
        [Required]
        public Guid UserId { get; protected set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; protected set; }
        [Required]
        public DateTime CreatedAt { get; protected set; }
        [Required]
        public DateTime StartDate { get; protected set; }
        [Required]
        public DateTime EndDate { get; protected set; }
        [Required]
        public decimal RentPrice { get; protected set; }
        
        protected Rental() { }

        public Rental(Guid id, Guid carId, string carName, decimal pricePerDay, decimal pricePerHour, Guid userId, string userName, DateTime start, DateTime end)
        {
            Id = id;
            SetCar(carId, carName);
            SetUser(userId, userName);
            CreatedAt = DateTime.UtcNow;
            SetDates(start, end);
            SetRentPrice(pricePerDay, pricePerHour);
        }
        public void SetCar(Guid carId, string carName)
        {
            CarId = carId;
            CarName = carName;
        }

        public void  SetUser(Guid userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public void SetDates(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                throw new Exception($"Rental with id: {Id} must have a end date greater than start date.");
            }
            StartDate = start;
            EndDate = end;
        }

        public void SetRentPrice(decimal pricePerDay, decimal pricePerHour)
        {
            var days = (decimal)(EndDate - StartDate).Days;
            var hours = (decimal)(EndDate - StartDate).Hours;
            RentPrice = days * pricePerDay + hours * pricePerHour;
        }
    }
}
