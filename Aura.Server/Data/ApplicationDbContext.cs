namespace Aura.Server.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Entities;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationDbContext : IdentityDbContext<AuraUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Author> Authors { get; set; } = null!;

        public DbSet<Album> Albums { get; set; } = null!;

        public DbSet<Playlist> Playlists { get; set; } = null!;

        public DbSet<Track> Tracks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuraUser>(b =>
            {
                b.HasMany(u => u.SavedTracks)
                    .WithMany(p => p.Users);

                b.HasMany(u => u.SavedAlbums)
                    .WithMany(p => p.Users);

                b.HasMany(u => u.Playlists)
                    .WithOne(p => p.Owner);
            });

            modelBuilder.Entity<Author>(b =>
            {
                b.ToTable("Authors");

                b.HasMany(a => a.Tracks)
                    .WithOne(t => t.Author);

                b.HasMany(a => a.Albums)
                    .WithOne(a => a.Author);
            });

            modelBuilder.Entity<Playlist>(b =>
            {
                b.HasMany(p => p.Tracks)
                    .WithMany(t => t.Playlists);
            });

            modelBuilder.Entity<Album>(b =>
            {
                b.HasMany(p => p.Tracks)
                    .WithMany(t => t.Albums);
            });
        }
    }
}
