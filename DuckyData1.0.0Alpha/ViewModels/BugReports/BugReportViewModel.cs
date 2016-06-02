using DuckyData1._0._0Alpha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.ViewModels
{
    public class BugReportBase
    {
        public DateTime date { get; set; }
        [Required]
        [StringLength(100)]
        public string subject { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string submittedBy { get; set; }
        public string ContentType { get; set; }
        public string ContentName { get; set; }

    }

    public class BugReportDetails : BugReportBase
    {
        public int Id { get; set; }
        public ICollection<FollowUp> FollowUps { get; set; }
        public ApplicationUser supportRep { get; set; }
    }

    public class BugReportAdd :BugReportBase
    {
        public ApplicationUser regUser { get; set; }
    }
}