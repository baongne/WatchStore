using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBuy_Backend.Data.Configurations
{
    public class InventoryVoucherDetailConfiguration : IEntityTypeConfiguration<InventoryVoucherDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryVoucherDetail> entity)
        {
            entity.ToTable("inventory_voucher_details");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            entity.Property(e => e.ReceivingUnitPrice)
                .IsRequired()
                .HasColumnName("receiving_unit_price");
        }
    }
}
