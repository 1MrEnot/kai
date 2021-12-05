namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;

    public class Track {

        public Track()
        {
            File = Array.Empty<byte>();
            Cover = Array.Empty<byte>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] File { get; set; }

        public int Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte[] Cover { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }

        public virtual ICollection<AuraUser> Users { get; set; }
    }
}