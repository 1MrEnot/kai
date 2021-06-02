namespace Aura.Server.Data
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Models;

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

            modelBuilder.Entity<AuraUser>().ToTable("Users");
            modelBuilder.Entity<Author>().ToTable("Authors");


            modelBuilder
                .Entity<Track>()
                .HasMany(t => t.SavedBy)
                .WithMany(u => u.SavedTracks)
                .UsingEntity(j => j.ToTable("SavedTracks"));

            modelBuilder
                .Entity<Album>()
                .HasMany(t => t.SavedBy)
                .WithMany(u => u.SavedAlbums)
                .UsingEntity(j => j.ToTable("SavedAlbums"));

            modelBuilder
                .Entity<Author>()
                .HasMany<Track>()
                .WithOne(t => t.Author)
                .HasForeignKey(el => el.AuthorId);

            modelBuilder
                .Entity<Author>()
                .HasMany<Album>()
                .WithOne(t => t.Author)
                .HasForeignKey(el => el.AuthorId);


        }
    }
}
