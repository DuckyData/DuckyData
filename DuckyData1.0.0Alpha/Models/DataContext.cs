using DuckyData1._0._0Alpha.Controllers;
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
        //public DbSet<RegisteredUser> RegisteredUsers { get; set; }

        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<BugReport> Reports { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Audio> Audio { get; set; }
        public DbSet<Video> Video { get; set; }
    }

    public class StoreInitializer : DropCreateDatabaseAlways<DataContext>
    //public class StoreInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        //private Manager m = new Manager();
        //private ApplicationDbContext db = new ApplicationDbContext();
        protected override void Seed(DataContext context)
        {
            /*var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("P@ssw0rd");

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
            */
            var msg = new Message
            {
                date = DateTime.Now,
                sendTo = "TestRecipient",
                Subject = "Test Subject",
               // user = user,
                body = "<strong>Testing</strong>"
            };
           // msg.user = user;
            context.Messages.Add(msg);
            //db.Users.Add(user);
            context.SaveChanges();
            //db.SaveChanges();
        }

    }

    
}