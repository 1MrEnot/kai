namespace Aura.Server.Models
{
    using System;

    public record AlbumReleaseModel(Guid Id, string Title, DateTime ReleaseDate, AuthorModel Author, TrackModel[] Tracks);
}