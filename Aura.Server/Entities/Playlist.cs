namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Playlist {

        public Playlist()
        {
            Tracks = new List<Track>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        [Required]
        public virtual AuraUser Owner { get; set; } = null!;

        public virtual ICollection<Track> Tracks { get; set; }
    }
}