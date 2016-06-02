using DuckyData1._0._0Alpha.Controllers;
using DuckyData1._0._0Alpha.Factory.Account;
using DuckyData1._0._0Alpha.ViewModels.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        {
        }
        public DbSet<User_Activation_Code> User_Activation_Codes { get; set; }
        public DbSet<userAdd> userAddCollection { get; set; }

        /*
        public DbSet<Message> Messages { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<BugReport> Reports { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Audio> Audio { get; set; }
        public DbSet<Video> Video { get; set; }
        */
        
    }

    public class StoreInitializer : DropCreateDatabaseAlways<DataContext>
    //public class StoreInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            /*

            var user = new ApplicationUser
            {
                Email = "Admin@DuckyData.ca",
                EmailConfirmed = true,
                UserName = "Admin@DuckyData.ca",
                PasswordHash = password,
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                firstName = "Mike",
                lastName = "Doherty",
                banned = "No",
                flagged = "No",
                gagged = "No",
            };
            
            var msg = new Message
            {
                SentDate = DateTime.Now,
                Recipient = "TestRecipient",
                Subject = "Test Subject",
                //User = user,
                Body = "<strong>Testing</strong>"
            };
            //msg.user = user;
            context.Messages.Add(msg);
            //db.Users.Add(user);
            context.SaveChanges();
        */
        }
        

    }

}