using SongPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace SongPlayer.BusinessLayer
{
    public class BusinessLayerx
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public PlaylistViewModel GetPlaylist(int? id,Playlist playlist)
        {
           
            
           
            var Results = from b in db.myFiles
                          select new
                          {
                              b.FileId,
                              b.FileName,
                              b.Album.AlbumName,
                              b.Album.Author,
                             
                              Checked = ((from ab in db.FilesToPlaylists where (ab.PlaylistID == id) & (ab.FileID == b.FileId) select ab).Count() > 0)

                          };
            var MyViewModel = new PlaylistViewModel();
            MyViewModel.PlaylistId = id.Value;
            MyViewModel.PlaylistName = playlist.PlaylistName;
          
            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.FileId, Name = item.FileName, Checked = item.Checked,Album = item.AlbumName,Autor = item.Author });
            }
            MyViewModel.Songs = MyCheckBoxList;
           
            return MyViewModel;
           
        }

        public PlaylistViewModel EditPlaylist(int? id,Playlist playlist)
        {
           
           
           
            var Results = from b in db.myFiles
                          select new
                          {
                              b.FileId,
                              b.FileName,
                              b.Album.AlbumName,
                              b.Album.Author,
                              Checked = ((from ab in db.FilesToPlaylists where (ab.PlaylistID == id) & (ab.FileID == b.FileId) select ab).Count() > 0)

                          };
            var MyViewModel = new PlaylistViewModel();
            MyViewModel.PlaylistId = id.Value;
            MyViewModel.PlaylistName = playlist.PlaylistName;
            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.FileId, Name = item.FileName, Checked = item.Checked, Album = item.AlbumName, Autor = item.Author });
            }
            MyViewModel.Songs = MyCheckBoxList;
            return MyViewModel;
        }
      

    }
}