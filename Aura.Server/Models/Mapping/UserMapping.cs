namespace Aura.Server.Models.Mapping
{
    using System.Linq;
    using Entities;

    public static class UserMapping
    {
        public static UserModel MapModel(this AuraUser user)
        {
            return new(
                user.Id,
                user.UserName,
                user.SavedAlbums.Select(a => a.MapAlbumModel()).ToList(),
                user.SavedTracks.Select(t => t.MapTrackModel()).ToList());
        }
    }
}