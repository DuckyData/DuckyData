using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Models
{
    public class Video : MediaFile
    {
        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; }

        public string Producer { get; set; }

        public List<string> Cast { get; set; }

        public byte[] Poster { get; set; }
    }

    public class Audio : MediaFile
    {
        public DateTime ReleaseDate { get; set; }

        public string Artist { get; set; }

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

        [Required]
        public string Type { get; set; }

        public Audio Audio { get; set; }

        public Video Video { get; set; }

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
        [StringLength(2000)]
        public string Body { get; set; }
        
        public ApplicationUser User { get; set; }

        //attachment attributes
        public byte[] Attachment { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }

    }

    public class History
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public MediaFile MediaFile { get; set; }

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
        [StringLength(2000)]
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

    public class BugReport
    {
        public BugReport()
        {
            this.FollowUps = new List<FollowUp>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public ApplicationUser RegisteredUser { get; set; }

        public ApplicationUser TechSupport { get; set; }

        [Required]
        public string SubmittedBy { get; set; }

        [Required]
        public DateTime SubmittedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        [StringLength(2000)]
        public string Body { get; set; }

        [Required]
        public Category Category { get; set; }

        public ICollection<FollowUp> FollowUps { get; set; }

        // Attachment Attributes
        public byte[] Attachment { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }
    }

    public class FollowUp
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        //attachment attributes
        public byte[] Attachment { get; set; }

        public string ContentType { get; set; }

        public string ContentName { get; set; }

        public BugReport Report { get; set; }
    }

}
