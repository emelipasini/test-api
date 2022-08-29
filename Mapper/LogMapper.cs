using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Models;

namespace Mapper
{
    public class LogMapper : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("logs")
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName("id")
                .IsRequired();

            builder
                .Property(a => a.Entity)
                .HasColumnName("entity")
                .IsRequired();

            builder
                .Property(a => a.Action)
                .HasColumnName("action")
                .IsRequired();

            builder
                .Property(a => a.Error)
                .HasColumnName("error")
                .IsRequired();
        }
    }
}
