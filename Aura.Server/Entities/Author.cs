namespace Aura.Server.Entities
{
    using System.Collections.Generic;

    public class Author : AuraUser {

        public Author()
        {
            Tracks = new List<Track>();
            Albums = new List<Album>();
        }

        public Author(AuraUser user)
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

            Tracks = new List<Track>();
            Albums = new List<Album>();
        }


        public string Nickname { get; set; } = null!;

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        /*
        public abstract Cover AddCover(byte[] file, string name);
        public abstract void DeleteCover(Cover cover);

        public abstract Track AddTrack(string name, byte[] file, DateTime releaseDate, Cover cover, Author[] otherAuthors);
        public abstract void DeleteTrack(Track track);

        public abstract Album AddAlbum(string name, Track[] tracks, DateTime releaseDate, Cover cover, Author[] otherAuthors);
        public abstract void DeleteAlbum(Album album);
        public abstract void DeleteTrackFromAlbum(Track track, Album album);
        public abstract void ChangeOrderingInAlbum(Album album, Track track, int position);*/
    }
}