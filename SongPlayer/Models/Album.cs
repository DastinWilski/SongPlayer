using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SongPlayer.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public string Author { get; set; }


        public virtual ICollection<Song> Songs { get; set; }
    }
}