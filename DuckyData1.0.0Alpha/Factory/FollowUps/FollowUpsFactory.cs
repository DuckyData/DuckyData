using DuckyData1._0._0Alpha.Models;
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
    }
}