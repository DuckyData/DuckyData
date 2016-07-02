using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace DuckyData1._0._0Alpha.Factory.BugReports
{
    public class BugRereportFactory
    {
        private DatabaseConnection conn = new DatabaseConnection();
        private DataContext database;
        private ApplicationDbContext appDB;

        public BugRereportFactory()
        {
            getDatabase();
        }

        public void getDatabase()
        {
            if(this.appDB == null)
            {
                appDB = new ApplicationDbContext();
            }
        }

        // function to get bugreport list with filters
        public IEnumerable<BugReportList> getBugReports(string query) {
            getDatabase();
            var bugList = from b in appDB.BugReports select b;
            if(!String.IsNullOrEmpty(query))
            {
                bugList = bugList.Where(b => b.subject.Contains(query)
                                      || b.submittedBy.Contains(query)
                                      || b.body.Contains(query));
            }
            bugList = bugList.OrderBy(b => b.date);
            return Mapper.Map<IEnumerable<BugReportList>>(bugList);
        }

        public BugReport findBugReprtById(int? id) {

            BugReport bug = appDB.BugReports.Include("FollowUps").FirstOrDefault(b => b.Id == id);
   
            if(bug != null)
            {
                return bug; 
            }
            else
            {
                return null;
            }
        }

        public BugReport findBugReprtOnlyById(int? id) {
            BugReport bug = appDB.BugReports.FirstOrDefault(b => b.Id == id);

            if(bug != null)
            {
                return bug;
            }
            else
            {
                return null;
            }
        }

        public BugReport findBugReprtForEdit(int? id) {
            BugReport bug = appDB.BugReports.Include("supportRep").FirstOrDefault(b => b.Id == id);

            if(bug != null)
            {
                return bug;
            }
            else
            {
                return null;
            }
        }

        // function to create bug report
        public void createBugReport(BugReportBase report, string userId) {
            getDatabase();
            BugReport bug = new BugReport();
            bug = Mapper.Map<BugReport>(report);
            bug.date = DateTime.Now;
            ApplicationUser user = appDB.Users.First(u => u.Id == userId);
            bug.submittedBy = user.firstName +" "+user.lastName;
            bug.regUser = user;

            

            appDB.BugReports.Add(bug);
            appDB.SaveChanges();
        }

        public bool closeTheBugReport(int id) {
            var bug = appDB.BugReports.FirstOrDefault(b => b.Id == id);
            if(bug == null) {
                return false;
            }

            bug.status = "Close";
            appDB.SaveChanges();
            return true;
        }

    }
}