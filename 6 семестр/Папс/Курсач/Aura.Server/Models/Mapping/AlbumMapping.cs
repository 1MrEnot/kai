namespace Aura.Server.Models.Mapping
{
    using System.Linq;
    using Data;
    using Entities;


    public static class AlbumMapping
    {
        public static AlbumModel MapAlbumModel(this Album album)
        {
            return new(album.Id, album.Name, album.Author.MapAuthorModel());
        }

        public static AlbumReleaseModel MapAlbumReleaseModel(this Album album,
            TrackDataDbContext trackDataDbContext)
        {
            return new(
                album.Id,
                album.Name,
                album.ReleaseDate,
                album.Author.MapAuthorModel(),
                album.Tracks.Select(t => t.MapTrackModel(trackDataDbContext)).ToList()
                );
        }
    }
}