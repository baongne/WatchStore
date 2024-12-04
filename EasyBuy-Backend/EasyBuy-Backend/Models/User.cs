using EasyBuy_Backend.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Backend.Models
{
    public class User
    {
		public int Id { get; set; }

		public string Name { get; set; }
	
		public string Email { get; set; }

		public string? Phone { get; set; }

		public string Password { get; set; }

        public string? Address { get; set; }

        public UserStatus Status { get; set; }

        public UserRole Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cart>? Carts { get; set; }
    }
}
