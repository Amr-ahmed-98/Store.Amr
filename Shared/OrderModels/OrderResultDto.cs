using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public class OrderResultDto
    {






        // Id
        public Guid Id { get; set; }
        // Email user that order
        public string UserEmail { get; set; }

        // Shipping Address
        public AddressDto ShippingAddress { get; set; }

        // Navigational property
        // Order Items
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        
        // Delivery Method
        public string DeliveryMethod { get; set; }


        // Payment Status
        public string PaymentStatus { get; set; } 

        // Sub Total
        public decimal subTotal { get; set; }

        // Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        // Payment
        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal Total { get; set; }
    }
}
