using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace DuckyData1._0._0Alpha.Models
{

    public class MediaResponse
    {
        public MediaResponse()
        {
            queryURL = "";
            fileURL = "";
        }
        public string queryURL { get; set; }
        public string fileURL { get; set; }
        public string fileName { get; set; }
        //public string fileType { get; set; }
        public bool statusCode { get; set; }
        public bool mimeStatusCode { get; set; }
    }

    public class fileInput
    {
        public HttpPostedFileBase input { get; set; }
        public byte[] bytes { get; set; }
    }

    public class acr
    {
        public string path { get; set; }
        public string album { get; set; }
        public string[] artists { get; set; }
        public string title { get; set; }
        public string duration { get; set; }
        public string[] genres { get; set; }
        public string producer { get; set; }
        public string director { get; set; }
        public string releaseDate { get; set; }
        public byte[] albumArt { get; set; }
        public string artMime { get; set; }
        public uint track { get; set; }
        public byte[] fileBytes { get; set; }

    }

    //
    public class Video :MediaFile
    {
        public DateTime releaseDate { get; set; }
        public string director { get; set; }
        public string producer { get; set; }
        public string cast { get; set; }
        public byte[] poster { get; set; }
    }

    public class Audio :MediaFile
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

    [Table("BugReports")]
    public class BugReport
    {
        public BugReport()
        {
            this.FollowUps = new List<FollowUp>();
        }
        [Key]
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
    public class MusicFavourite
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MusicURL { get; set; }
        public string MusicTitle { get; set; }
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
        public string VideoURL { get; set; }
    }

    public class LastFmAlbumArt
    {
        public static string AbsUrlOfArt(string album, string artist)
        {
            Lastfm.Services.Session session = new Lastfm.Services.Session("c4c683261f4ee4b4b757b60c3b473d2d", "8cedb96695f0824b3855cb9c600a20bd");
            Lastfm.Services.Artist lArtist = new Lastfm.Services.Artist(artist, session);
            Lastfm.Services.Album lAlbum = new Lastfm.Services.Album(lArtist, album, session);

            return lAlbum.GetImageURL();
        }

        public static Image AlbumArt(string album, string artist)
        {
            Stream stream = null;
            try
            {
                WebRequest req = WebRequest.Create(AbsUrlOfArt(album, artist));
                WebResponse response = req.GetResponse();
                stream = response.GetResponseStream();
                Image img = Image.FromStream(stream);

                return img;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }
    }


    public class SimpleFile
    {
        public SimpleFile(string Name, Stream Stream)
        {
            this.Name = Name;
            this.Stream = Stream;
        }
        public string Name { get; set; }
        public Stream Stream { get; set; }
    }

    public class SimpleFileAbstraction : TagLib.File.IFileAbstraction
    {
        private SimpleFile file;

        public SimpleFileAbstraction(SimpleFile file)
        {
            this.file = file;
        }

        public string Name
        {
            get { return file.Name; }
        }

        public System.IO.Stream ReadStream
        {
            get { return file.Stream; }
        }

        public System.IO.Stream WriteStream
        {
            get { return file.Stream; }
        }

        public void CloseStream(System.IO.Stream stream)
        {
            stream.Position = 0;
        }

    }

}