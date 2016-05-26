using DuckyData1._0._0Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Factory
{
    public class DatabaseConnection
    {
        private DataContext db = new DataContext();

        public DataContext getConnection() {
            if (this.db == null) {
                this.db = new DataContext();
            }
            return this.db;
        }
    }
}