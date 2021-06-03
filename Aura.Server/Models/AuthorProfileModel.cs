namespace Aura.Server.Models
{
    using System;
    using System.Collections.Generic;

    public record AuthorProfileModel(Guid Id, string Name, List<TrackModel> Tracks, List<AlbumModel> Albums);
}