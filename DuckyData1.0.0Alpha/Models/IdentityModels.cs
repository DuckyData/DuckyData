using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace DuckyData1._0._0Alpha.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string flagged { get; set; }
        public string gagged { get; set; }
        public string banned { get; set; }

        public ApplicationUser(string email, string password)
        {
            this.EmailConfirmed = true;
            this.PhoneNumberConfirmed = true;
            this.TwoFactorEnabled = true;
            this.LockoutEnabled = true;
            this.AccessFailedCount = 0;
            this.UserName = email;
            this.Email = email;
            this.PasswordHash = password;
        }

        public ApplicationUser() {}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=DataContext", throwIfV1Schema: false)
        {
           
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<DuckyData1._0._0Alpha.ViewModels.Account.userAdd> userAdds { get; set; }

        public System.Data.Entity.DbSet<DuckyData1._0._0Alpha.ViewModels.Account.adminEditUser> adminEditUsers { get; set; }
    } 
}