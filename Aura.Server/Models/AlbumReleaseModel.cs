namespace Aura.Server.Models
{
    public record AlbumReleaseModel(int Id, AuthorModel[] Authors, string Title, TrackModel[] Tracks);
}