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

namespace DuckyData1._0._0Alpha.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        static private string uID = HttpContext.Current.User.Identity.GetUserId();
        static private string uNm = HttpContext.Current.User.Identity.Name;

        
        // MESSAGE MANAGEMENT - BEGIN
        public MessageBase SendMessage(MessageAdd newItem)
        {
            //var user = ds.Users.SingleOrDefault(i => i.Id == uID);
            newItem.UserId = uID;
            newItem.UserName = uNm;
            newItem.viewed = false;
            var addedItem = ds.Messages.Add(Mapper.Map<Message>(newItem));

            if (newItem.Attachments != null)
            {
                byte[] logoBytes = new byte[newItem.Attachments.ContentLength];
                newItem.Attachments.InputStream.Read(logoBytes, 0, newItem.Attachments.ContentLength);


                addedItem.Attachment = logoBytes;
                addedItem.ContentType = newItem.Attachments.ContentType;
                string[] tmps = newItem.Attachments.FileName.Split(new char[] { '\\' });
                addedItem.ContentName = tmps[tmps.Count() - 1];
            }
          
                ds.SaveChanges();
            
                return (addedItem == null) ? null : Mapper.Map<MessageBase>(addedItem);
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

        public IEnumerable<MessageBase> Inbox()
        {
            var messages = ds.Messages.Where(x => x.Recipient == uNm);

            return (messages == null) ? null : Mapper.Map<IEnumerable<MessageBase>>(messages);
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

            if(toEdit == null) { return null; }
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
            var rmsg = omsg;
            rmsg.viewed = false;
            ds.Entry(omsg).CurrentValues.SetValues(rmsg);
            ds.SaveChanges();
        }

        public bool DeleteMessage(int id)
        {
            var toDel = ds.Messages.SingleOrDefault(i => i.Id == id);
            if (toDel == null)
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