namespace Aura.Server.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author : AuraUser {

        public Author()
        {
            Tracks = new List<Track>();
            Albums = new List<Album>();
        }

        public string Nickname { get; set; } = null!;

        public double AccumulatedMoney { get; set; }

        [Required]
        public BankCard DeductionsCard { get; set; } = null!;

        public ICollection<Track> Tracks { get; set; }

        public ICollection<Album> Albums { get; set; }

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