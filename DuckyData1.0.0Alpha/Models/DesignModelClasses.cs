using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Models
{
    public class fileInput
    {
        public HttpPostedFileBase input { get; set; }
        public byte[] bytes { get; set; }
    }

    public class acr
    {
        public string result { get; set; }
        public string album { get; set; }
        public string artist { get; set; }
        public string song { get; set; }
        public string duration { get; set; }
        public string genre { get; set; }


    }

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
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SentDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Recipient { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        public bool viewed { get; set; }

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

    
    public class Video
    {
        [Key]
        public int Id { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; }

        public string Producer { get; set; }

        public List<string> Cast { get; set; }

        public byte[] Poster { get; set; }
    }

    public class Audio
    {
        [Key]
        public int Id { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Artist { get; set; }

        public List<string> ContributingArtists { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public int TrackNumber { get; set; }

        public string Producer { get; set; }

        public byte[] AlbumArt { get; set; }
    }

    public class MediaFile
    {
        [Key]
        public int Id { get; set; }

        public int QueryCount { get; set; }

        [Required]
        public string Name { get; set; }

        public Audio Audio { get; set; }

        public Video Video { get; set; }

        public string ImageName { get; set; }

        public string ImageType { get; set; }

        public byte[] File { get; set; }

        public string FileType { get; set; }
    }

    
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SentDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Recipient { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        public bool viewed { get; set; }

        //attachment attributes
        public byte[] Attachment { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }

    }

    public class History
    {
        [Key]
        public int Id { get; set; }

        public MediaFile file { get; set; }
               
        [Required]
        public ApplicationUser User { get; set; }
    }

    public class Suggestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime SentDate { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public Category Category { get; set; }

        public string Notes { get; set; }
    }

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
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
        [Display(Name ="Data Submitted")]
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

    public class MusicFavourite {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MusicURL { get; set; }
        public string MusicTitle {get; set;}
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumCover { get; set; }
    }

    public class VideoFavourite
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoImg { get; set; }
    }
}
