namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class Playlist {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual AuraUser Owner { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}