using DuckyData1._0._0Alpha.Controllers;
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
    }
}