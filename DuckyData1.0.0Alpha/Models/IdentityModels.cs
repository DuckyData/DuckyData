using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuckyData1._0._0Alpha.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.suggestions = new List<Suggestion>();
            this.reports = new List<BugReport>();
            this.messages = new List<Message>();
            this.history = new List<History>();
        }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string flagged { get; set; }
        public string gagged { get; set; }
        public string banned { get; set; }

        public ICollection<Suggestion> suggestions { get; set; }
        public ICollection<BugReport> reports { get; set; }
        public ICollection<Message> messages { get; set; }
        public ICollection<History> history { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    /*
    public class RegisteredUser : ApplicationUser
    {
        public RegisteredUser()
        {
            this.suggestions = new List<Suggestion>();
            this.reports = new List<BugReport>();
        }
        public string flagged { get; set; }
        public string gagged { get; set; }
        public string banned { get; set; }

        public ICollection<Suggestion> suggestions { get; set; }
        public ICollection<BugReport> reports { get; set; }
    }

    public class TechnicalSupport : ApplicationUser
    {
        public int repId { get; set; }
    }

    public class WebsiteAdministrator : ApplicationUser
    {
        public int adminId { get; set; }
    }

    public class AnonymousClient : ApplicationUser
    {
        public string ipAddress { get; set; }
    }
    */
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DataContext", throwIfV1Schema: false)
        {
           
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }


    // code for activate a user

    public class User_Activation_Code{

        [Key]
        public int id { get; set; }
        public string User_Account { get; set; }
        public string Activation_Code { get; set; }
    }
}