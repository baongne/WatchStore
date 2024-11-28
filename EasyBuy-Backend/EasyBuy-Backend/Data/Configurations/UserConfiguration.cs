using EasyBuy_Backend.Models;
using EasyBuy_Backend.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBuy_Backend.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users");

			entity.HasKey(e => e.Id);

			entity.Property(e => e.Id)
				.IsRequired()
				.HasColumnName("id");

			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50)
				.HasColumnName("name");
			entity.Property(e => e.Email)
				.IsRequired()
				.HasColumnName("email");

			entity.Property(e => e.Phone)
				.HasMaxLength(15)
				.HasColumnName("phone");

			entity.Property(e => e.Password)
				.IsRequired()
				.HasColumnName("password");

			entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("address");

            entity.Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserStatus)Enum.Parse(typeof(UserStatus), v))
                .HasColumnName("status");

            entity.Property(e => e.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v))
                .HasColumnName("role");
        }
    }
}
