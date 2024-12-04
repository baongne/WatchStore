using EasyBuy_Backend.Models;
using EasyBuy_Backend.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBuy_Backend.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("categories");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(250);

            entity.Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (CategoryStatus)Enum.Parse(typeof(CategoryStatus), v))
                .HasColumnName("status");
        }
    }
}
