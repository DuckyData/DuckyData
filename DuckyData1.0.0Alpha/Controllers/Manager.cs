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
using ACRCloud;
using static System.Net.WebRequestMethods;
using System.IO;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        private AccountController a = new AccountController();
        static private string uID = HttpContext.Current.User.Identity.GetUserId();
        static private string uNm = HttpContext.Current.User.Identity.Name;

        public acr RunQuery(fileInput input)
        {
            Program p = new Program();
            var result =  p.go(input);
            
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
    }
}