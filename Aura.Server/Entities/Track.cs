namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;

    public class Track {

        public Track()
        {
            File = Array.Empty<byte>();
            SavedBy = new List<AuraUser>();
        }

        public Guid Id { get; set; }

        public virtual Author Author { get; set; }

        public Guid AuthorId { get; set; }

        public string Name { get; set; } = null!;

        public byte[] File { get; set; }

        public int Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte[] Cover { get; set; } = null!;
        
        public virtual ICollection<AuraUser> SavedBy { get; set; }
    }
}