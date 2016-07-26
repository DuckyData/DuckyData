using System.Collections.Generic;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.Factory.FavouriteFactory;
using PagedList;
using DuckyData1._0._0Alpha.ViewModels.MusicFetch;
using AutoMapper;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class MusicFavouritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FavouriteFactory favouriteFactory = new FavouriteFactory();

        // GET: MusicFavourites
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

            IEnumerable<MusicFavouriteDisplay> videos = favouriteFactory.getAudioList(userId,searchString);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(videos.ToPagedList(pageNumber,pageSize));
        }

        // GET: MusicFavourites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicFavourite musicFavourite = favouriteFactory.findFavAudioById(id);
            if (musicFavourite == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<MusicFavouriteDisplay>(musicFavourite));
        }

        // POST: MusicFavourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if(id == 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool res = favouriteFactory.removeFavAudioById(id);
            if(res == false)
            {
                return HttpNotFound();
            }

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
