namespace Aura.Server.Models.Mapping
{
    using Data;
    using Entities;

    public static class TrackMapping
    {
        public static TrackModel MapTrackModel(this Track track, TrackData trackData)
        {
            return new(
                track.Id,
                track.Author.MapAuthorModel(),
                track.Name,
                trackData.Duration,
                track.ReleaseDate,
                trackData.Cover);
        }

        public static TrackModel MapTrackModel(this Track track, TrackDataDbContext trackDataDbContext)
        {
            var trackData = trackDataDbContext.TrackDatas.Find(track.Id);
            return track.MapTrackModel(trackData);
        }
    }
}