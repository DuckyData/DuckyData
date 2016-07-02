using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DuckyData1._0._0Alpha.Models;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class FollowUpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FollowUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUp followUp = db.FollowUps.Find(id);
            if (followUp == null)
            {
                return HttpNotFound();
            }
            return View(followUp);
        }

        // GET: FollowUps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FollowUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( FollowUp followUp)
        {
            if (ModelState.IsValid)
            {
                var bug = db.BugReports.Include("FollowUps").FirstOrDefault(b => b.Id == 1);
                followUp.report = bug;

                db.FollowUps.Add(followUp);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(followUp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
