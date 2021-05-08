namespace Aura.Server.Models
{
    public record TrackModel(int Id, AuthorModel[] Authors, string Title, int Duration);
}