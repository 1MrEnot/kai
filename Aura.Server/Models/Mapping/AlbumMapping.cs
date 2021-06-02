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
    }
}