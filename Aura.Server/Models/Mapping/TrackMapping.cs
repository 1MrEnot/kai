namespace Aura.Server.Models.Mapping
{
    using Entities;

    public static class TrackMapping
    {
        public static TrackModel MapTrackModel(this Track track)
        {
            return new(
                track.Id,
                track.Author.MapAuthorModel(),
                track.Name,
                track.Duration,
                track.ReleaseDate,
                track.Cover);
        }
    }
}