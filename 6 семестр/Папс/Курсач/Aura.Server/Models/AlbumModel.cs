namespace Aura.Server.Models
{
    using System;

    public record AlbumModel(Guid Id, string Title, AuthorModel Author);
}