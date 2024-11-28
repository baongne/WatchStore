using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EasyBuy_Backend.Models.Enums;

namespace EasyBuy_Backend.Data.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> entity)
        {
            entity.ToTable("vouchers");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id"); 

            entity.Property(e => e.VoucherName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("voucher_name"); 

            entity.Property(e => e.Discount)
                .IsRequired()
                .HasColumnName("discount"); 

            entity.Property(e => e.Unit)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("unit"); 

            entity.Property(e => e.DateFrom)
                .IsRequired()
                .HasColumnName("date_from"); 

            entity.Property(e => e.DateTo)
                .IsRequired()
                .HasColumnName("date_to"); 

            entity.Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (VoucherStatus)Enum.Parse(typeof(VoucherStatus), v))
                .HasColumnName("status"); 
        }
    }
}
