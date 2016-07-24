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
    public class VideoFavouritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VideoFavourites
        public ActionResult Index()
        {
            return View(db.VideoFavourites.ToList());
        }

        // GET: VideoFavourites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoFavourite videoFavourite = db.VideoFavourites.Find(id);
            if (videoFavourite == null)
            {
                return HttpNotFound();
            }
            return View(videoFavourite);
        }

        // GET: VideoFavourites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoFavourites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,VideoId,VideoTitle,VideoImg")] VideoFavourite videoFavourite)
        {
            if (ModelState.IsValid)
            {
                db.VideoFavourites.Add(videoFavourite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(videoFavourite);
        }

        // GET: VideoFavourites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoFavourite videoFavourite = db.VideoFavourites.Find(id);
            if (videoFavourite == null)
            {
                return HttpNotFound();
            }
            return View(videoFavourite);
        }

        // POST: VideoFavourites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,VideoId,VideoTitle,VideoImg")] VideoFavourite videoFavourite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoFavourite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoFavourite);
        }

        // GET: VideoFavourites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoFavourite videoFavourite = db.VideoFavourites.Find(id);
            if (videoFavourite == null)
            {
                return HttpNotFound();
            }
            return View(videoFavourite);
        }

        // POST: VideoFavourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoFavourite videoFavourite = db.VideoFavourites.Find(id);
            db.VideoFavourites.Remove(videoFavourite);
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
    }
}
