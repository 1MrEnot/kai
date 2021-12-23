namespace Aura.Server.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class TrackDataDbContext : DbContext
    {
        public TrackDataDbContext(DbContextOptions<TrackDataDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TrackData> TrackDatas { get; set; } = null!;
    }
}