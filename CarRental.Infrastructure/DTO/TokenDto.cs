using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.DTO
{
    public class TokenDto
    {
        public string JWT { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public long Expires { get; set; }
    }
}
