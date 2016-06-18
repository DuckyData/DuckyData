﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.Factory.BugReports;
using DuckyData1._0._0Alpha.ViewModels;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Web.Routing;
using PagedList;
using DuckyData1._0._0Alpha.Factory.FollowUps;
using DuckyData1._0._0Alpha.ViewModels.FollowUps;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class BugReportsController :Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private BugRereportFactory bugRereportFactory = new BugRereportFactory();
        private FollowUpsFactory followUpsFactory = new FollowUpsFactory();
        // GET: BugReports
        public ActionResult Index(string query)
        {

            IEnumerable<BugReportList> bugList = bugRereportFactory.getBugReports(query);
            int pageSize = 3;
            int pageNumber = 1;
            return View(bugList.ToPagedList(pageNumber,pageSize));
        }

        // GET: BugReports/Details/5
        // view buy report and followup associaed
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bugReport = bugRereportFactory.findBugReprtById(id);
            if(bugReport == null)
            {
                return HttpNotFound();
            }
            
            return View(bugReport);
        }

        // GET: BugReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BugReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BugReportBase bugReport)
        {
            if(ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                bugRereportFactory.createBugReport(bugReport,userId);
                return RedirectToAction("Index");
            }

            return View(bugReport);
        }

        // GET: BugReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bugReport = bugRereportFactory.findBugReprtForEdit(id);

            if(bugReport == null)
            {
                return HttpNotFound();
            }
            return View(bugReport);
        }

        // POST: BugReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BugReport bugReport, string command)
        {
            if(command == "Save the Change")
            {
                if(ModelState.IsValid)
                {
                    db.Entry(bugReport).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else {
                bugRereportFactory.closeTheBugReport(bugReport.Id);
            }
            return RedirectToAction("Details","BugReports",new { id = bugReport.Id });
        }

        // GET: BugReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = db.BugReports.Find(id);
            if(bugReport == null)
            {
                return HttpNotFound();
            }
            return View(bugReport);
        }

        // POST: BugReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BugReport bugReport = db.BugReports.Find(id);
            db.BugReports.Remove(bugReport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        // GET: BugReports/FollowUps/5
        public ActionResult FollowUps(int? id) {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = bugRereportFactory.findBugReprtOnlyById(id);

            FollowUpAddForm newFollowUp = new FollowUpAddForm();
            newFollowUp.report = bugReport;
            if(bugReport == null)
            {
                return HttpNotFound();
            }

            return View(newFollowUp);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
