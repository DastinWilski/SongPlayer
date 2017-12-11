using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongPlayer.Models
{
    public class SongsToPlaylist
    {
        public int SongsToPlaylistID { get; set; }
        public int FileID { get; set; }
        public int PlaylistID { get; set; }

        public virtual Song Song { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}