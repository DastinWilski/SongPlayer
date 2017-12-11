using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongPlayer.Models
{
    public class PlaylistViewModel
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public string Album { get; set; }
        public string Autor { get; set; }
        public List<CheckBoxViewModel> Songs { get; set; }
    }
}