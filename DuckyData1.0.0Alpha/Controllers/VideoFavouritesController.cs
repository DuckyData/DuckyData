using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using PagedList;
using DuckyData1._0._0Alpha.Models;
using Microsoft.AspNet.Identity;
using DuckyData1._0._0Alpha.Factory.FavouriteFactory;
using DuckyData1._0._0Alpha.ViewModels.VideoFetch;
using System.Linq;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class VideoFavouritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FavouriteFactory favouriteFactory = new FavouriteFactory();
        // GET: VideoFavourites
        [Authorize]
        public ActionResult Index(int? page)
        {
            string userId = User.Identity.GetUserId();

            var videoList = TempData["fVideoList"] as List<VideoFavouriteDisplay>;

            if(videoList == null)
            {
                videoList = favouriteFactory.getVideoList(userId,null);
            }
            
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(videoList.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult searchVideo(string searchString) {
            string userId = User.Identity.GetUserId();

            List<VideoFavouriteDisplay> audioList = favouriteFactory.getVideoList(userId,searchString);
            TempData["fVideoList"] = audioList.ToList();
            return RedirectToAction("Index");
        }

        // GET: VideoFavourites/Delete/5
        [Authorize]
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
        [Authorize]
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
