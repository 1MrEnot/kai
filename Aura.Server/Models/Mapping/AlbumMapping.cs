namespace Aura.Server.Models.Mapping
{
    using System.Linq;
    using Entities;


    public static class AlbumMapping
    {
        public static AlbumModel MapAlbumModel(this Album album)
        {
            return new(album.Id, album.Name, album.Author.MapAuthorModel());
        }

        public static AlbumReleaseModel MapAlbumReleaseModel(this Album album)
        {
            return new(
                album.Id,
                album.Name,
                album.ReleaseDate,
                album.Author.MapAuthorModel(),
                album.Tracks.Select(t => t.MapTrackModel()).ToList()
                );
        }

        public static AlbumModel MapAlbumModel(this AlbumReleaseModel album)
        {
            return new(album.Id, album.Title, album.Author);
        }

    }
}