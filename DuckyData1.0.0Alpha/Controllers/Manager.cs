using AutoMapper;
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
        private DataContext ds = new DataContext();
        /*
        public MessageAdd SendMessage(MessageAdd newItem)
        {
            //var user = 
            var addedItem = ds.Messages.Add(Mapper.Map<Message>(newItem));
            ds.SaveChanges();
            return (addedItem == null) ? null : Mapper.Map<MessageAdd>(addedItem);
        }
        */
    }
}