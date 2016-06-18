using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Models
{
    //
    public class Video : MediaFile
    {
        public DateTime releaseDate { get; set; }
        public string director { get; set; }
        public string producer { get; set; }
        public string cast { get; set; }
        public byte[] poster { get; set; }
    }

    public class Audio : MediaFile
    {
        public DateTime releaseDate { get; set; }
        public string artist { get; set; }
        public string album { get; set; }
        public string genre { get; set; }
        public int trackNumber { get; set; }
        public string producer { get; set; }
        public byte[] albumArt { get; set; }
    }

    public class MediaFile
    {
        public int Id { get; set; }
        //[Required]
        //public ApplicationUser user { get; set; }
        [Required]
        public string fileName { get; set; }
        [Required]
        public string mediaType { get; set; }
        public Audio audio { get; set; }
        public Video video { get; set; }
        public byte[] fileData { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        [Required]
        [StringLength(100)]
        public string sendTo { get; set; }
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }
        [Required]
        public string body { get; set; }
        public ApplicationUser user { get; set; }
        //attachment attributes
        public byte[] Attachment { get; set; }
        public string ContentType { get; set; }
        public string ContentName { get; set; }

    }

    public class History
    {
        public int Id { get; set; }
        [Required]
        public MediaFile file { get; set; }
        [Required]
        public ApplicationUser user { get; set; }
    }

    public class Suggestion
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        [Required]
        [StringLength(100)]
        public string subject { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public string category { get; set; }
        public string notes { get; set; }

        public ApplicationUser regUser { get; set; }
    }

    [Table("BugReports")]
    public class BugReport
    {
        public BugReport()
        {
            this.FollowUps = new List<FollowUp>();
        }
        [Key]
        public int Id { get; set; }
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
        [Display(Name ="Submitted By")]
        public string submittedBy { get; set; }
        public ApplicationUser regUser { get; set; }
        [Display(Name ="Assign To")]
        public string assignTo { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        public ApplicationUser supportRep { get; set; }
        public virtual ICollection<FollowUp> FollowUps { get; set; }

    }
    [Table("FollowUps")]
    public class FollowUp
    {
        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:f}")]
        public DateTime TimeStamp { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual BugReport report { get; set; }
        public string CreatedBy { get; set; }
    }

}
