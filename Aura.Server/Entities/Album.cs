namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album {

        public Album()
        {
            Authors = new List<Author>();
            Tracks = new List<Track>();
            SavedBy = new List<AuraUser>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public Cover Cover { get; set; } = null!;

        public ICollection<Author> Authors { get; set; }

        public ICollection<Track> Tracks { get; set; }

        public ICollection<AuraUser> SavedBy { get; set; }
    }
}