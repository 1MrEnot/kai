namespace Aura.Server.Models.Mapping
{
    using System.Linq;
    using Data;
    using Entities;

    public static class UserMapping
    {
        public static UserModel MapModel(this AuraUser user, TrackDataDbContext trackDataDbContext)
        {
            return new(
                user.Id,
                user.UserName,
                user.SavedAlbums.Select(a => a.MapAlbumModel()).ToList(),
                user.SavedTracks.Select(t => t.MapTrackModel(trackDataDbContext)).ToList());
        }
    }
}