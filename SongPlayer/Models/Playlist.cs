using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SongPlayer.Models
{
    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }

        public string PlaylistName { get; set; }

      
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser  { get; set; }

        public virtual ICollection<SongsToPlaylist> SongsToPlaylists { get; set; }
    }
}