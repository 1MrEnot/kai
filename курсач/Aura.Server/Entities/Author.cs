namespace Aura.Server.Entities
{
    using System.Collections.Generic;

    public class Author : AuraUser {

        public Author()
        {
            Tracks = new List<Track>();
            Albums = new List<Album>();
        }

        public Author(AuraUser user) : this()
        {
            Id = user.Id;
            UserName = user.UserName;
            NormalizedUserName = user.NormalizedUserName;
            Email = user.Email;
            NormalizedEmail = user.NormalizedEmail;
            EmailConfirmed = user.EmailConfirmed;
            PasswordHash = user.PasswordHash;
            SecurityStamp = user.SecurityStamp;
            ConcurrencyStamp = user.ConcurrencyStamp;
            PhoneNumber = user.PhoneNumber;
            PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            TwoFactorEnabled = user.TwoFactorEnabled;
            LockoutEnabled = user.LockoutEnabled;
            AccessFailedCount = user.AccessFailedCount;

            SavedAlbums = user.SavedAlbums;
            SavedTracks = user.SavedTracks;
            Playlists = user.Playlists;
        }


        public string Nickname { get; set; } = null!;

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}