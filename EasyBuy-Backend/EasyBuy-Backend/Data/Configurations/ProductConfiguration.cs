using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EasyBuy_Backend.Models.Enums;


namespace EasyBuy_Backend.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("products");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasColumnName("id");
            
			entity.Property(e => e.Code)
				.IsRequired()
				.HasColumnName("code");
            
			entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("product_name");

            entity.Property(e => e.PriceToSell)
                .IsRequired()
                .HasColumnName("price_to_sell");

            entity.Property(e => e.ImportPrice)
                .IsRequired()
                .HasColumnName("import_price");

            entity.Property(e => e.Discount)
                .HasColumnName("discount");

            entity.Property(e => e.Model)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("model");

            entity.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("color");

            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("gender");

            entity.Property(e => e.Description)
                .HasColumnName("description");

            entity.Property(e => e.ProductImg)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("product_img");

            entity.Property(e => e.CanDel)
                .HasColumnName("can_del");

            entity.Property(e => e.StockQuantity)
                .HasColumnName("stock_quantity");

            entity.Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (ProductStatus)Enum.Parse(typeof(ProductStatus), v))
                .HasColumnName("status");
        }
    }
}
