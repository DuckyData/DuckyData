using DuckyData1._0._0Alpha.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {     
            //UNCOMMENT AND ENTER EMAIL TO GRANT ADMIN
            /*
            using (var context = new ApplicationDbContext())
            { 
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                roleManager.Create(new IdentityRole("Admin"));

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.FindByEmail("msdoherty@myseneca.ca");
                userManager.AddToRole(user.Id, "Admin");
                context.SaveChanges();
            } */ 
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "DuckyData Address.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        [ChildActionOnly]
        public ActionResult _AdminTools()
        {
            return PartialView();
        }

    }
}