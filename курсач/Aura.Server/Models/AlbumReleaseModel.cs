namespace Aura.Server.Models
{
    using System;
    using System.Collections.Generic;

    public record AlbumReleaseModel(Guid Id, string Title, DateTime ReleaseDate, AuthorModel Author, List<TrackModel> Tracks);
}