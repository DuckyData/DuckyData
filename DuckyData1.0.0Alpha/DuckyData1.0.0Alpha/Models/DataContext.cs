using DuckyData1._0._0Alpha.Controllers;
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
        private Manager m = new Manager();
        protected override void Seed(DataContext context)
        {
            context.Suggestions.Add(new Suggestion
            {
                date = DateTime.Now,
                subject = "Test subject",
                body = "Test Body",
                category = "Test",
                notes = "test notes"
            });
            context.SaveChanges();
        }

    }

}