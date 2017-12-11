using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SongPlayer.Models
{
    public class Song
    {
        [Key]
        public int FileId { get; set; }

        public string FileName { get; set; }

        public string Path { get; set; }

        

        public int AlbumID { get; set; }

        public int GenreID { get; set; }

        public int myTypeID { get; set; }
        

        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual myType myType { get; set; }

        public virtual ICollection<SongsToPlaylist> SongsToPlaylists { get; set; }
    }
}