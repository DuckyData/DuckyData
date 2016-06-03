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

        static string uID = HttpContext.Current.User.Identity.GetUserId();

        public MessageAdd SendMessage(MessageAdd newItem)
        {
            //var user = ds.Users.SingleOrDefault(i => i.Id == uID);
            newItem.UserId = uID;
            var addedItem = ds.Messages.Add(Mapper.Map<Message>(newItem));
            ds.SaveChanges();
            return (addedItem == null) ? null : Mapper.Map<MessageAdd>(addedItem);
        }

        public ICollection<Message> inbox()
        {
            var messages = (ICollection<Message>)ds.Messages.Where(x => x.UserId == uID);
            return (messages == null) ? null : messages;
        } 
        
    }
}