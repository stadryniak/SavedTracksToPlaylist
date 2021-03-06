﻿using SpotifyAPI.Web;
using System;

namespace SavedTracksToPlaylist.Modules
{
    class PlaylistUpdater
    {
        internal void PlaylistUpdate( SpotifyWebAPI spotify, string playlistId )
        {
            var tracksUri = HelperFunctions.FindFirstCommonTrackUri( spotify, HelperFunctions.GetSavedTracksUris( spotify ), playlistId );

            if ( tracksUri.Count != 0 )
            {
                HelperFunctions.InsertTracks( spotify, tracksUri, playlistId );
            }
            else
            {
                Console.WriteLine( "Nothing to add." );
            }
        }
    }
}
