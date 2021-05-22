using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aura.Server.Data
{
    using System;
    using Entities;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationDbContext : IdentityDbContext<AuraUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BankCard> Cards { get; set; } = null!;

        public DbSet<Author> Authors { get; set; } = null!;

        public DbSet<Album> Albums { get; set; } = null!;

        public DbSet<Playlist> Playlists { get; set; } = null!;

        public DbSet<Track> Tracks { get; set; } = null!;

        public DbSet<Cover> Covers { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuraUser>().ToTable("Users");
            modelBuilder.Entity<Author>().ToTable("Authors");

            modelBuilder.Entity<AuraUser>()
                .HasMany(u => u.SavedTracks)
                .WithMany(t => t.SavedBy)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSavedTrack",
                    b => b
                        .HasOne<Track>()
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade),
                    b => b
                        .HasOne<AuraUser>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade));

            modelBuilder.Entity<AuraUser>()
                .HasMany(u => u.SavedAlbums)
                .WithMany(t => t.SavedBy)
                .UsingEntity<Dictionary<string, object>>(
                    "UserSavedAlbum",
                    b => b
                        .HasOne<Album>()
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade),
                    b => b
                        .HasOne<AuraUser>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade));

            modelBuilder.Entity<Author>()
                .HasMany(u => u.Tracks)
                .WithMany(t => t.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "TrackAuthor",
                    b => b
                        .HasOne<Track>()
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade),
                    b => b
                        .HasOne<Author>()
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade));

            modelBuilder.Entity<Author>()
                .HasMany(u => u.Albums)
                .WithMany(t => t.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AlbumAuthor",
                    b => b
                        .HasOne<Album>()
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade),
                    b => b
                        .HasOne<Author>()
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade));
        }
    }
}