namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
            SavedBy = new List<AuraUser>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public virtual Author Author { get; set; }

        public Guid AuthorId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<AuraUser> SavedBy { get; set; }
    }
}