namespace Aura.Server.Models
{
    using System;

    public record AuthorProfileModel(Guid Id, string Name, TrackModel[] Tracks, AlbumModel[] Albums);
}