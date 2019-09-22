using LandmarkRemark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LandmarkRemark.Persistence.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(50);
            builder.Property(m => m.AddedOn).IsRequired();
            builder.Property(m => m.Location).HasColumnType("geometry").IsRequired();
        }
    }
}