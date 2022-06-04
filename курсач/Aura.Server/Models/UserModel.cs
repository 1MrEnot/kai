namespace Aura.Server.Models
{
    using System;
    using System.Collections.Generic;

    public record UserModel(Guid Id, string Name, List<AlbumModel> SavedAlbums, List<TrackModel> SavedTracks);
}