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
        [Display(Name = "Video")]
        public string VideoId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string VideoTitle { get; set; }
        [Display(Name = "Cover Art")]
        public string VideoImg { get; set; }
        public string VideoURL { get; set; }
    }

    public class VideoFavouriteDisplay
    {
        public int Id { set; get; }
        [Required]
        [Display(Name = "Video")]
        public string VideoId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string VideoTitle { get; set; }
        [Display(Name = "Cover Art")]
        public string VideoImg { get; set; }
        public string VideoURL { get; set; }
    }

    public class SearchModel {
        public string query { get; set; }
        public int page { set; get; }
    }
}