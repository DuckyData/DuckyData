using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class ControlPanelsController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult _Admin()
        {
            return PartialView();
        }

        [Authorize(Roles = "TechSupport")]
        public ActionResult _TechSupport()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "TechSupport")]
        public ActionResult TechSupport()
        {
            return View();
        }
    }
}