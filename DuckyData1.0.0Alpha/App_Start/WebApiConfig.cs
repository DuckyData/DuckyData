using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DuckyData1._0._0Alpha.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // other settings removed for clarity

            config.EnableCors(new EnableCorsAttribute("*","*","*"));
        }
    }
}