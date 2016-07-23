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
    public class VideoFetchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VideoFetch
        public ActionResult Index()
        {
            return View();
        }

        // GET: VideoFetch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = new Video();
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: VideoFetch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoFetch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fileName,mediaType,fileData,releaseDate,director,producer,cast,poster")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.MediaFiles.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(video);
        }

        // GET: VideoFetch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = new Video();
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: VideoFetch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fileName,mediaType,fileData,releaseDate,director,producer,cast,poster")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: VideoFetch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = new Video();
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: VideoFetch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = new Video();
            db.MediaFiles.Remove(video);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult oauth2callback() {
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
    }
}
