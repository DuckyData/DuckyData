using DuckyData1._0._0Alpha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuckyData1._0._0Alpha.ViewModels
{
    public class BugReportBase
    {
        // view model for basic bug report
        public DateTime date { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Subject")]
        public string subject { get; set; }
        [Required]
        [AllowHtml]
        [Display(Name = "Description")]
        public string body { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }
        [Display(Name = "Submitted By")]
        public string submittedBy { get; set; }
        [Display(Name = "Content Type")]
        public string ContentType { get; set; }
        [Display(Name = "Content Name")]
        public string ContentName { get; set; }
        [Display(Name = "Status")]
        public string status;

    }

    // view model for display bug report detailes
    public class BugReportDetails : BugReportBase
    {
        public int Id { get; set; }
        public ICollection<FollowUp> FollowUps { get; set; }

        [Display(Name = "Tech Support")]
        public ApplicationUser supportRep { get; set; }
    }

    public class BugReportList :BugReportBase {
        public int Id { set; get; }
        [Display(Name = "Tech Support")]
        public string assignTo { set; get; }
    }

    // view model for create bug report
    public class BugReportAdd :BugReportBase
    {
        public ApplicationUser regUser { get; set; }
    }

    public class BugReportEdit {

        public int Id { get; set; }
        [Display(Name = "Data Submitted")]
        public DateTime date { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Subject")]
        public string subject { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string body { get; set; }
        [Required]
        [Display(Name = "category")]
        public string category { get; set; }
        [Required]
        [Display(Name = "Submitted By")]
        public string submittedBy { get; set; }
        public ApplicationUser regUser { get; set; }
        [Display(Name = "Assign To")]
        public SelectList assignTo { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
    }
}