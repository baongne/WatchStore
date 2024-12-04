using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBuy_Backend.Data.Configurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> entity)
        {
            entity.ToTable("order_lines");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            entity.Property(e => e.UnitPrice)
                .IsRequired()
                .HasColumnName("unit_price");
        }
    }
}
