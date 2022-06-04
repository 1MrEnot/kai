namespace Aura.Server.Entities
{
    using System;

    public class TrackData
    {
        public Guid Id { get; set; }

        public byte[] File { get; set; }

        public int Duration { get; set; }

        public byte[] Cover { get; set; }
    }
}