using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SongPlayer.Models
{
    public class myType
    {
        public int myTypeId { get; set; }

        public string TypeName { get; set; }




        public virtual ICollection<Song> Songs { get; set; }
    }
}