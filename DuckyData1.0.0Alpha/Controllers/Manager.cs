using AutoMapper;
using DuckyData1._0._0Alpha;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ACRCloud;
using static System.Net.WebRequestMethods;
using System.IO;
using DuckyData1._0._0Alpha.Factory.Account;
using DuckyData1._0._0Alpha.ViewModels.Account;
using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using TagLib;
using TagLib.Id3v2;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        private AccountController a = new AccountController();
        static private string uID = HttpContext.Current.User.Identity.GetUserId();
        static private string uNm = HttpContext.Current.User.Identity.Name;
        static private AccountFactory af = new AccountFactory();
        IEnumerable<userRole> AuthUsers()
        {
            IEnumerable<userFlags> users = af.getUserList(null);
            ICollection<userRole> aUsers = null;
            foreach (var user in users)
            {
                if (Roles.IsUserInRole("Admin"))
                {
                    aUsers.Add(new userRole
                    {
                        Id = user.Id,
                        Role = "Admin",
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
                }
                if (Roles.IsUserInRole("TechSupport"))
                {
                    aUsers.Add(new userRole
                    {
                        Id = user.Id,
                        Role = "TechSupport",
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
                }
            }
            IEnumerable<userRole> sorted = null;
            if (aUsers != null)
            {
                sorted = aUsers.OrderBy(r => r.Role).AsEnumerable();
            }
            return (sorted == null) ? null : sorted;
        }

        public string GetMimeType(Image i)
        {
            var imgguid = i.RawFormat.Guid;
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == imgguid)
                    return codec.MimeType;
            }
            return "image/unknown";
        }

        public acr RunQuery(fileInput input)
        {

            var ext = Path.GetExtension(input.input.FileName);
            Program p = new Program();

            var result =  p.go(input);

            var art = LastFmAlbumArt.AlbumArt(result.album, result.artists[0]);
            var ms = new MemoryStream();
            if (art != null)
            {
                art.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                result.albumArt = ms.ToArray();
                result.artMime = GetMimeType(art);
            }
            var dir = "~/media/";
            
            if (input.input.ContentType.Contains("video"))
            {

            }
            else if (input.input.ContentType.Contains("audio"))
            {
                
                result.path = Path.Combine(HttpContext.Current.Server.MapPath(dir + result.title + ext));

                input.input.SaveAs(result.path);

                TagLib.File f = TagLib.File.Create(result.path); 


                //f.Tag.
                TagLib.Id3v2.Tag t = (TagLib.Id3v2.Tag)f.GetTag(TagTypes.Id3v2); // You can add a true parameter to the GetTag function if the file doesn't already have a tag.
                if (t == null)
                {
                    t = new TagLib.Id3v2.Tag();
                }


                t.Album = result.album;
                int index = result.album.LastIndexOf("(");
                if (index > 0)
                {
                    result.album = result.album.Substring(0, index);
                }
                index = result.album.LastIndexOf("[");
                if (index > 0)
                {
                    result.album = result.album.Substring(0, index);
                }
                t.Performers = result.artists;
                //t.Track = result.track;
                t.Genres = result.genres;
                t.Title = result.title;
                t.Copyright = result.producer;
                if (result.albumArt != null)
                {
                    Picture pic = new Picture(result.albumArt);
                    t.Pictures = new Picture[1] { pic };
                }

                f.Save();
                string i = "";
            }

                    return result;
        }

        public string Between(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }


        // MESSAGE MANAGEMENT - BEGIN
        public MessageBase SendMessage(MessageAdd newItem)
        {
            //var user = ds.Users.SingleOrDefault(i => i.Id == uID);
           // var usr = a.UserManager.Users.FirstOrDefault(i => i.Id == uID);
           // if(usr.gagged) { return null; }
            newItem.UserId = uID;
            newItem.UserName = uNm;
            newItem.viewed = false;
            Message msg = new Message();
            msg.Recipient = newItem.Recipient;
            msg.SentDate = newItem.SentDate;
            msg.Subject = newItem.Subject;
            msg.UserId = newItem.UserId;
            msg.UserName = newItem.UserName;
            msg.viewed = newItem.viewed;
            msg.Body = newItem.Body;
            var addedItem = ds.Messages.Add(msg);
            //var addedItem = ds.Messages.Add(Mapper.Map<Message>(newItem));
           if (newItem.Attachments != null)
            {
                byte[] logoBytes = new byte[newItem.Attachments.ContentLength];
                newItem.Attachments.InputStream.Read(logoBytes, 0, newItem.Attachments.ContentLength);
                

                //addedItem.Attachment = logoBytes;
                addedItem.ContentType = newItem.Attachments.ContentType;


                //string[] tmps = newItem.Attachments.FileName.Split(new char[] { '\\' });
                addedItem.ContentName = newItem.Attachments.FileName;
                ds.SaveChanges();
                string path = HttpContext.Current.Server.MapPath("~/images/MsgAttach/" + addedItem.Id + newItem.Attachments.FileName);
                System.IO.File.WriteAllBytes(path, logoBytes);
            }
            else
            {

                ds.SaveChanges();
            }
            
                return (addedItem == null) ? null : Mapper.Map<MessageBase>(addedItem);
        }

        public IEnumerable<MessageBase> AllMsg()
        {
            var fetched = ds.Messages.AsEnumerable();
            ICollection<MessageBase> messages = new List<MessageBase>();
            foreach(var item in fetched)
            {
                var tmp = new MessageBase();
                tmp.Id = item.Id;
                tmp.Attachment = item.Attachment;
                tmp.Body = item.Body;
                tmp.ContentName = item.ContentName;
                tmp.ContentType = item.ContentType;
                tmp.Recipient = item.Recipient;
                tmp.SentDate = item.SentDate;
                tmp.Subject = item.Subject;
                tmp.UserId = item.UserId;
                tmp.UserName = item.UserName;
                tmp.viewed = item.viewed;
                messages.Add(tmp);
            }

            return (messages == null) ? null : messages;
        }

        public MessageBase GetMessageById(int id)
        {
            var fetchedObject = (ds.Messages.SingleOrDefault(i => i.Id == id));
            var newstate = fetchedObject;
            if (fetchedObject.viewed == false && fetchedObject.Recipient == uNm)
            {
                newstate.viewed = true;
                ds.Entry(fetchedObject).CurrentValues.SetValues(newstate);
                ds.SaveChanges();
            }
            return (fetchedObject == null) ? null
                : Mapper.Map < MessageBase > (fetchedObject);
        }

        

        public IEnumerable<MessageBase> Outbox()
        {
            var messages = ds.Messages.Where(x => x.UserId == uID ).OrderBy(d => d.SentDate);


            return (messages == null) ? null : Mapper.Map<IEnumerable<MessageBase>>(messages);
        } 

        public IEnumerable<Message> Inbox()
        {
            var messages = ds.Messages.SqlQuery("Select * from dbo.Messages").Where(x => x.Recipient == uNm);

            return (messages == null) ? null : messages;
        }

        public IEnumerable<MessageBase> UnreadMessages()
        {
            var messages = ds.Messages.Where(v => v.viewed == false);
            return (messages == null) ? null : Mapper.Map<IEnumerable<MessageBase>>(messages);
        }

        public IEnumerable<MessageBase> ReadMessages()
        {
            var messages = ds.Messages.Where(b => b.viewed == true);
            return (messages == null) ? null : Mapper.Map<IEnumerable<MessageBase>>(messages);
        }

        public IEnumerable<MessageBase> QueryMessagesSubject(string phrase)
        {
            var messages = ds.Messages.Where(s => s.Subject == phrase);
            return (messages == null) ? null : Mapper.Map<IEnumerable<MessageBase>>(messages);
        }

        public IEnumerable<MessageBase> QueryMessagesBody(string phrase)
        {
            var messages = ds.Messages.Where(b => b.Body == phrase);
            return (messages == null) ? null : Mapper.Map<IEnumerable<MessageBase>>(messages);
        }

        public MessageBase EditMessage(MessageEdit toApply)
        {
            var toEdit = ds.Messages.SingleOrDefault(i => i.Id == toApply.Id);

            if(toEdit == null || (uNm != toEdit.UserName && !HttpContext.Current.User.IsInRole("admin"))) { return null; }
            else
            {
                toApply.Attachment = toEdit.Attachment;
                toApply.ContentType = toEdit.ContentType;
                toApply.ContentName = toEdit.ContentName;
                toApply.Id = toEdit.Id;
                ds.Entry(toEdit).CurrentValues.SetValues(toApply);
                ds.SaveChanges();
                return Mapper.Map<MessageBase>(toEdit);
            }
        }

        public void MarkAsUnread(int id)
        {
            var omsg = ds.Messages.SingleOrDefault(i => i.Id == id);
            if (uNm == omsg.Recipient)
            {
                var rmsg = omsg;
                rmsg.viewed = false;
                ds.Entry(omsg).CurrentValues.SetValues(rmsg);
                ds.SaveChanges();
            }
        }

        public bool DeleteMessage(int id)
        {
            var toDel = ds.Messages.SingleOrDefault(i => i.Id == id);
            if (toDel == null || (uNm != toDel.UserName && !HttpContext.Current.User.IsInRole("admin")))
            {
                return false;
            }
            else
            {
                ds.Messages.Remove(toDel);
                ds.SaveChanges();
                return true;
            }
        }
        // MESSAGE MANAGEMENT - END



        public void SampleTaglib(Stream song, string songFileName)
        {
            //Get the tags
            TagLib.Tag tags = FileTagReader(song, songFileName);

            //We can read and display info from it
            //TextBlock1.Text = tags.Title;
            //TextBlock2.Text = tags.Album;

            //Sample 2
            tags.Comment = "Read and edited using SampleTaglib app by Zumicts";

            //Reading picture
            MemoryStream ms = new MemoryStream(tags.Pictures[0].Data.Data);
            //System.Windows.Media.Imaging.BitmapImage bi = new BitmapImage();
            //.SetSource(ms);

            //Writing picture
            //albmStream would be a stream containing picture data
            //create the picture for the album cover
            //TagLib.Picture picture = new TagLib.Picture(new TagLib.ByteVector(albmStream.ToArray()));

            //create Id3v2 Picture Frame
            //TagLib.Id3v2.AttachedPictureFrame albumCoverPictFrame = new TagLib.Id3v2.AttachedPictureFrame(Ipicture);
            //set the type of picture (front cover)
            //albumCoverPictFrame.Type = TagLib.PictureType.FrontCover;

            //Id3v2 allows more than one type of image, just one needed
            //TagLib.IPicture[] pictFrames = { albumCoverPictFrame };

            //tags.Pictures = pictFrames;
        }

        public static TagLib.Tag FileTagReader(Stream stream, string fileName)
        {
            //Create a simple file and simple file abstraction
            var simpleFile = new SimpleFile(fileName, stream);
            var simpleFileAbstraction = new SimpleFileAbstraction(simpleFile);
            /////////////////////

            //Create a taglib file from the simple file abstraction
            var mp3File = TagLib.File.Create(simpleFileAbstraction);

            //Get all the tags
            TagLib.Tag tags = mp3File.Tag;

            //Save and close
            mp3File.Save();
            mp3File.Dispose();

            //Return the tags
            return tags;
        }

        public static Stream FileTagEditor(Stream stream, string fileName, TagLib.Tag newTag)
        {
            //Create a simple file and simple file abstraction
            var simpleFile = new SimpleFile(fileName, stream);
            var simpleFileAbstraction = new SimpleFileAbstraction(simpleFile);
            /////////////////////

            //Create a taglib file from the simple file abstraction
            var mp3File = TagLib.File.Create(simpleFileAbstraction);

            //Copy the all the tags to the file (overwrite if exist)
            newTag.CopyTo(mp3File.Tag, true);
            //Pictures tag had to be done seperately
            //During testing sometimes it didn't copy
            mp3File.Tag.Pictures = newTag.Pictures;

            //save it and close it
            mp3File.Save();
            mp3File.Dispose();

            //Return the stream back (now edited with the new tags)
            return stream;
        }



    }
}