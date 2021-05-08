namespace Aura.Server.Models
{
    public record AuthorProfileModel(int Id, string Name, byte[] Avatar, TrackModel[] Tracks, AlbumModel[] Albums);
}