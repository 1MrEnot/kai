namespace Aura.Server.Models
{
    using System;

    public record TrackModel(Guid Id, AuthorModel Author, string Title, int Duration);
}