using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Core.Domain
{
    public class User : Entity
    {
        private static List<string> _roles = new List<string>
        {
            "user", "admin"
        };
        [Required]
        [MaxLength(20)]
        public string Role { get; protected set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; protected set; }
        [Required]
        [MaxLength(200)]
        public string Email { get; protected set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; protected set; }
        [Required]
        public DateTime CreatedAt { get; protected set; }

        protected User() { }

        public User(Guid id, string role, string name,
            string email, string password)
        {
            Id = Id;
            SetRole(role);
            SetName(name);
            SetEmail(email);
            SetPassword(password);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new Exception($"User can not have an empty role.");
            }
            role = role.ToLowerInvariant();
            if (!_roles.Contains(role))
            {
                throw new Exception($"User can not have a role: '{role}'.");
            }
            Role = role;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User can not have an empty name.");
            }
            Name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"User can not have an empty email.");
            }
            Email = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"User can not have an empty password.");
            }
            Password = password;
        }
    }
}
