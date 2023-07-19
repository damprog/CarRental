using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.DTO.Rentals
{
    public class RentalDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string CarName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentPrice { get; set; }
    }
}
