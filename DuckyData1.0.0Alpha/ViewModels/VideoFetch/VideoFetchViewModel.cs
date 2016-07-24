using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.ViewModels.VideoFetch
{
    public class VideoFavouriteAdd
    {
        public string UserId { get; set; }
        [Required]
        public string VideoId { get; set; }
        [Required]
        public string VideoTitle { get; set; }
        public string VideoImg { get; set; }
    }
}