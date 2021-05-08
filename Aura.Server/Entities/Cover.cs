namespace Aura.Server.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Cover {

        public Cover()
        {
            File = Array.Empty<byte>();
        }

        public Guid Id { get; set; }

        [Required]
        public byte[] File { get; set; }

        public string Name { get; set; } = null!;

        public int Width { get; set; }

        public int Height { get; set; }

        [Required]
        public Author Owner { get; set; } = null!;
    }
}