using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EasyBuy_Backend.Models.Enums;
using static EasyBuy_Backend.Models.Order;

namespace EasyBuy_Backend.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("orders"); 

            entity.HasKey(e => e.Id); 

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id"); 

            entity.Property(e => e.OrderDate)
                .IsRequired()
                .HasColumnName("order_date");

            entity.Property(e => e.ShippingFee)
                .IsRequired()
                .HasColumnName("shipping_fee"); 

            entity.Property(e => e.OrderDiscount)
                .IsRequired()
                .HasColumnName("order_discount"); 

            entity.Property(e => e.OrderTotal)
                .IsRequired()
                .HasColumnName("order_total"); 

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("address"); 

            entity.Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v))
                .HasColumnName("status");
        }
    }
}
