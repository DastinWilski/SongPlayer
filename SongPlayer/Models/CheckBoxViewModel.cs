using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongPlayer.Models
{
    public class CheckBoxViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public string Autor { get; set; }
        public string Album { get; set; }
    }
}