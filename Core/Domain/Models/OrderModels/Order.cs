using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {

        }

        public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            this.subTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }





        // Id
        // Email user that order
        public string UserEmail { get; set; }

        // Shipping Address
        public Address ShippingAddress { get; set; }

        // Navigational property
        // Order Items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Navigational property
        // Delivery Method
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; } // FK


        // Payment Status
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        // Sub Total
        public decimal subTotal { get; set; }

        // Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        // Payment
        public string PaymentIntentId { get; set; }
    }
}
