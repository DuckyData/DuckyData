using DuckyData1._0._0Alpha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuckyData1._0._0Alpha.ViewModels.FollowUps
{
    public class FollowUpAddForm
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string Title { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000)]
        public string Description { get; set; }
        public BugReport report { get; set; }
    }
}