﻿using System;
using System.Collections.Generic;
using System.Linq;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;

namespace SavedTracksToPlaylist.Modules
{
    internal class Analyze
    {
        public List<string> GetSavedTrackProperties( SpotifyWebAPI spotify )
        {
            var tracksIds = HelperFunctions.GetSavedTracksUris( spotify );
            tracksIds = tracksIds.Select( s => s.Replace( "spotify:track:", "" ) ).ToList();

            var trackList = new List<SeveralTracks>();

            Console.WriteLine( "Generating list of tracks ids..." );
            for ( int i = 0; i < tracksIds.Count; i += 50 )
            {
                var count = 50;
                var flag = 0;
                if ( ( i + 50 ) > tracksIds.Count )
                {
                    count = tracksIds.Count - i;
                    i -= 50;
                    flag = 1;
                }
                if ( flag == 1 )
                {
                    break;
                }
                var shortIdList = tracksIds.GetRange( i, count );
                trackList.Add( spotify.GetSeveralTracks( shortIdList ) );
            }

            Console.WriteLine( "Track ids generated." );

            var features = new List<AudioFeatures>();

            tracksIds.ForEach( id =>
             {
                 Console.WriteLine( $"Getting features of: {id}" );
                 features.Add( spotify.GetAudioFeatures( id ) );
             } );

            var danceabilityFloats = new List<float>();
            var energyFloats = new List<float>();
            features.ForEach( track =>
             {
                 Console.WriteLine( $"Feature id: {track.Id}" );
                 Console.WriteLine( $"Danceability: {track.Danceability}" );
                 danceabilityFloats.Add( track.Danceability );
                 Console.WriteLine( $"Energy: {track.Energy}" );
                 energyFloats.Add( track.Energy );
             } );
            Console.WriteLine( $"Features count: {features.Count}" );
            Console.WriteLine( $"Track Ids count: {tracksIds.Count}" );
            Console.WriteLine();

            Console.WriteLine( $"Tracks average danceability: {danceabilityFloats.Average()}" );
            Console.WriteLine( $"Tracks average energy: {energyFloats.Average()}" );
            return null;
        }
    }
}
