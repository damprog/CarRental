using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Domain
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; protected set; }

        protected Entity() { 
            Id = Guid.NewGuid();
        }
    }
}
