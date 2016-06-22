using Microsoft.Owin.Mapping;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace DuckyData1._0._0Alpha
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
