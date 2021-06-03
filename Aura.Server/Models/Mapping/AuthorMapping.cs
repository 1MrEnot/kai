namespace Aura.Server.Models.Mapping
{
    using System.Linq;
    using Entities;

    public static class AuthorMapping
    {
        public static AuthorModel MapAuthorModel(this Author author)
        {
            return new(author.Id, author.Nickname);
        }

        public static AuthorProfileModel MapAuthorProfileModel(this Author author)
        {
            return new(
                author.Id,
                author.Nickname,
                author.Tracks.Select(t => t.MapTrackModel()).ToList(),
                author.Albums.Select(a => a.MapAlbumModel()).ToList()
                );
        }
    }
}