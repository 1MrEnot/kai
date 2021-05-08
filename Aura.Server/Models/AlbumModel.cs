namespace Aura.Server.Models
{
    public record AlbumModel(int Id, string Title, AuthorModel[] Authors);
}