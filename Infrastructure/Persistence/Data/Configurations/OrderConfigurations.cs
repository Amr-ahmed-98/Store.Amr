using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // I need to make the value of shippingAddress not a new table it's part of Order table
            builder.OwnsOne(O => O.ShippingAddress, address => address.WithOwner());

            builder.HasMany(O => O.OrderItems)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            // i want to store the name of the flag itself like (pending,PaymentRecieved,PaymentFailed) not as int

            builder.Property(O => O.PaymentStatus)
                .HasConversion(S => S.ToString(), S => Enum.Parse<OrderPaymentStatus>(S));

            builder.Property(o => o.subTotal)
                .HasColumnType("decimal(18,4)");

        }
    }
}
