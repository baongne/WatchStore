using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBuy_Backend.Data.Configurations
{
    public class InventoryVoucherConfiguration : IEntityTypeConfiguration<InventoryVoucher>
    {
        public void Configure(EntityTypeBuilder<InventoryVoucher> entity)
        {
            entity.ToTable("inventory_vouchers");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnName("date");

            entity.Property(e => e.Total)
                .IsRequired()
                .HasColumnName("total");
        }
    }
}
