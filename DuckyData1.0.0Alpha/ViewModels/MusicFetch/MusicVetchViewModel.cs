using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.ViewModels.MusicFetch
{
    public class MusicFavouriteAdd
    {
        public string UserId { get; set; }
        [Required]
        public string MusicURL { get; set; }
        [Required]
        public string MusicTitle { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumCover { get; set; }
    }
}