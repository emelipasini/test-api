using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Models;

namespace Mapper
{
    public class StreetMapper: IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.ToTable("streets")
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName("id")
                .IsRequired();

            builder
                .Property(a => a.Name)
                .HasColumnName("name")
                .IsRequired();

            builder
                .Property(a => a.City)
                .HasColumnName("city")
                .IsRequired();
        }
    }
}
