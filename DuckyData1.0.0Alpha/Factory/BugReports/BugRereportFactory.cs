using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Factory.BugReports
{
    public class BugRereportFactory
    {
        private DatabaseConnection conn = new DatabaseConnection();
        private DataContext database;
        private ApplicationDbContext userDB = new ApplicationDbContext();

        public BugRereportFactory()
        {
            this.database = conn.getConnection();
        }

        public void getDatabase()
        {
            if(this.database == null)
            {
                this.database = conn.getConnection();
            }
        }

        // public IEnumerable<> getBugReportList() {



        //} 

        public void createBugReport(BugReportBase report) {
            getDatabase();
            BugReport bug = new BugReport();
            bug = Mapper.Map<BugReport>(report);
            bug.date = DateTime.Now;
            ApplicationUser user = userDB.Users.First(u => u.Email == "zhuzhaohu.daniel@gmail.com");
            bug.submittedBy = "daniel";
            bug.regUser = user;
            bug.supportRep = user;
            //bug.regUser = HttpContext.Current.User.Identity.
            userDB.BugReports.Add(bug);
            userDB.SaveChanges();
        }
    }
}