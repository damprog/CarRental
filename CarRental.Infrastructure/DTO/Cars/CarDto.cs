using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.DTO.Cars
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PricePerHour { get; set; }

        // Here can be added more fields that can be displayed in presentation layer
    }
}
