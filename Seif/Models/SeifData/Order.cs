using System;
using System.Collections.Generic;

namespace Seif.Models.SeifData
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDAte { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Paid { get; set; }
        public bool Delivery { get; set; }
        public bool Closed { get; set; }
        public Guid CheckOrderTocken { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } 
    }
}