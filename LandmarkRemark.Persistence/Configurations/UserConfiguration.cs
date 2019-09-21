using LandmarkRemark.Domain;
using LandmarkRemark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LandmarkRemark.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(m => m.LastName).IsRequired().HasMaxLength(100);
            builder.Property(m => m.UserName).IsRequired().HasMaxLength(50);
            builder.Property(m => m.PasswordHash).IsRequired();
            builder.Property(m => m.PasswordSalt).IsRequired();

            builder.HasIndex(m => m.UserName).IsUnique();
        }
    }
}