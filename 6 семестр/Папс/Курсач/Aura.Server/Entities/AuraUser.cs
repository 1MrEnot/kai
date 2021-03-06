namespace Aura.Server.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class AuraUser: IdentityUser<Guid> {

        public virtual ICollection<Track> SavedTracks { get; set; }

        public virtual ICollection<Album> SavedAlbums { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }

        /*public abstract void AddTrackToFavorites(Track track);
        public abstract void RemoveTrackFromFavorites(Track track);

        public abstract void AddAlbumToFavorites(Album album);
        public abstract void RemoveAlbumFromFavorites(Album album);

        public abstract Playlist CreatePlaylist(string playlistName);
        public abstract void AddPlaylistToFavorites(Playlist playlist);
        public abstract void RemovePlaylistFromFavorites(Playlist playlist);
        public abstract void AddTrackToPlaylist(Track track, Playlist playlist);
        public abstract void ChangeOrderingInPlaylist(Playlist playlist, Track track, int position);

        public abstract void ListenToTrack(Track track);

        public abstract bool ProlongSubscription();
        public abstract void ChangeSubscriptionType(SubscriptionType subscriptionType);*/
    }
}