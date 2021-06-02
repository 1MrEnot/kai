namespace Aura.Server.Models
{
    using System;

    public record AlbumReleaseModel(Guid Id, AuthorModel Author, string Title, TrackModel[] Tracks);
}