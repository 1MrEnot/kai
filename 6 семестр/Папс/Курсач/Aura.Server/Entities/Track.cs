namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;

    public class Track {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }

        public virtual ICollection<AuraUser> Users { get; set; }
    }
}