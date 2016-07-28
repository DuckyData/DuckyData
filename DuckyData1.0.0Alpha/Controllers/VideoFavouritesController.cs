using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using PagedList;
using DuckyData1._0._0Alpha.Models;
using Microsoft.AspNet.Identity;
using DuckyData1._0._0Alpha.Factory.FavouriteFactory;
using DuckyData1._0._0Alpha.ViewModels.VideoFetch;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class VideoFavouritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FavouriteFactory favouriteFactory = new FavouriteFactory();
        // GET: VideoFavourites
        public ActionResult Index(string currentFilter,string searchString,int? page)
        {
            string userId = User.Identity.GetUserId();
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IEnumerable<VideoFavouriteDisplay> videos = favouriteFactory.getVideoList(userId, searchString);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(videos.ToPagedList(pageNumber,pageSize));
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
            if(videoFavourite == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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
