using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Domain
{
    [Table("Cars")]
    public class Car : Entity
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; protected set; }
        [Required]
        [MaxLength (1000)]
        public string Description { get; protected set; }
        [Required]
        public decimal PricePerDay { get; protected set; }
        [Required]
        public decimal PricePerHour { get; protected set; }
        [Required]
        public DateTime UpdatedAt { get; protected set; }

        protected Car() { }

        public Car(Guid id, string name, string description, decimal pricePerDay, decimal pricePerHour)
        {
            Id = id;
            SetName(name);
            SetDescription(description);
            SetPricePerDay(pricePerDay);
            SetPricePerHour(pricePerHour);
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"Car with id: '{Id}' can not have an empty name.");
            }
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception($"Car with id: '{Id}' can not have an empty description.");
            }
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetPricePerDay(decimal price)
        {
            if (price <= 0)
            {
                throw new Exception($"Car with id: '{Id}' can not have a price less or equal 0.");
            }
            PricePerDay = price;
            UpdatedAt = DateTime.UtcNow;
        }
        public void SetPricePerHour(decimal price)
        {
            if (price <= 0)
            {
                throw new Exception($"Car with id: '{Id}' can not have a price less or equal 0.");
            }
            PricePerHour = price;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
