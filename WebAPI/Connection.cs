using Microsoft.EntityFrameworkCore;

using Models;
using Mapper;

namespace WebAPI
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

        public DbSet<Street> Streets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseSerialColumns();

            new StreetMapper().Configure(builder.Entity<Street>());
            new LogMapper().Configure(builder.Entity<Log>());
        }
    }
}
