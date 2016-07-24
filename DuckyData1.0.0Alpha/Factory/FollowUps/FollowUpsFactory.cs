using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels.FollowUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Factory.FollowUps
{
    public class FollowUpsFactory
    {
        private DatabaseConnection conn = new DatabaseConnection();
        private ApplicationDbContext appDB;

        public void getDatabase()
        {
            if(this.appDB == null)
            {
                appDB = new ApplicationDbContext();
            }
        }

        public FollowUpsFactory()
        {
            getDatabase();
        }

        public IEnumerable<FollowUp> getFollowUpsByReportId(int reportId) {
            var followUpList = appDB.FollowUps.Include("report");
            if(followUpList == null) {
                return null;
            }else
            {
                return followUpList;
            }
        }

        public bool createNewFollowUp(FollowUpAddForm followUp, string userId) {
            FollowUp followUpToAdd;
            if(userId == null)
            {
                return false;
            }
            else {
                BugReport bug = appDB.BugReports.Include("supportRep").FirstOrDefault(f => f.Id == followUp.report.Id);
                if(bug == null)
                {
                    return false;
                }
                else {
                    ApplicationUser user = appDB.Users.First(u => u.Id == userId);
                    followUpToAdd = Mapper.Map<FollowUp>(followUp);
                    followUpToAdd.TimeStamp = DateTime.Now;
                    followUpToAdd.report = bug;
                    followUpToAdd.CreatedBy = user.firstName + " " + user.lastName;
                    appDB.FollowUps.Add(followUpToAdd);
                    appDB.SaveChanges();
                    return true;
                }
            }
        }

    }
}