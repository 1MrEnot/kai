namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;

    public class Track {

        public Track()
        {
            File = Array.Empty<byte>();
            Authors = new List<Author>();
            SavedBy = new List<AuraUser>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public byte[] File { get; set; }

        public int Duration { get; set; }

        public long ListenTimes { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Cover Cover { get; set; } = null!;

        public ICollection<Author> Authors { get; set; }

        public ICollection<AuraUser> SavedBy { get; set; }
    }
}