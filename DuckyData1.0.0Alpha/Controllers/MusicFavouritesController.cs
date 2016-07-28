using System.Collections.Generic;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.Factory.FavouriteFactory;
using PagedList;
using DuckyData1._0._0Alpha.ViewModels.MusicFetch;
using AutoMapper;
using System.Linq;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class MusicFavouritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FavouriteFactory favouriteFactory = new FavouriteFactory();

        // GET: MusicFavourites
        [Authorize]
        public ActionResult Index(int? page)
        {
            string userId = User.Identity.GetUserId();

            var audioList = TempData["fAudioList"] as List<MusicFavouriteDisplay>;

            if(audioList == null) {
                audioList = favouriteFactory.getAudioList(userId,null);
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(audioList.ToPagedList(pageNumber,pageSize));
        }

        [Authorize]
        public ActionResult searchAudio(string searchString) {
            string userId = User.Identity.GetUserId();
            List<MusicFavouriteDisplay> audioList = favouriteFactory.getAudioList(userId,searchString);
            TempData["fAudioList"] = audioList.ToList();

            return RedirectToAction("Index");
        }


        // GET: MusicFavourites/Delete/5
        [Authorize]
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
        [Authorize]
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
