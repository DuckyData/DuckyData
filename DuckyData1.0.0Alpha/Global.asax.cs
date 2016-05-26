using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DuckyData1._0._0Alpha
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //localdb
            // System.Data.Entity.Database.SetInitializer(new Models.StoreInitializer());

            Mapper.CreateMap<ApplicationUser, userAdd>();
            Mapper.CreateMap<userAdd, ApplicationUser>();
            Mapper.CreateMap<ApplicationUser, userBase>();
            Mapper.CreateMap<userAdd,ApplicationUser>();
        }
    }
}
