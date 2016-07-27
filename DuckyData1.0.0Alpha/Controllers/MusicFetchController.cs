using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels.MusicFetch;
using DuckyData1._0._0Alpha.Factory.FavouriteFactory;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class MusicFetchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager m = new Manager();
        private FavouriteFactory favouriteFactory = new FavouriteFactory();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Input()
        {
            var addForm = new fileInput();
            return View(addForm);
        }
        
       

        public ActionResult _MediaInput()
        {
            var addForm = new fileInput();
            return PartialView(addForm);
        }

        [HttpPost]
        public string _MediaInput(int? id, fileInput newItem)
        {
            if (ModelState.IsValid)
            {
                newItem.bytes = new byte[newItem.input.ContentLength];
                newItem.input.InputStream.Read(newItem.bytes, 0, newItem.input.ContentLength);
                var result = m.RunQuery(newItem);
                var album = result.album;
                string tmp = string.Format("http://localhost:8102/MusicFetch/Index?album={0}", album);
                return tmp;
            }
            return "";

        }

        [Authorize]
        public ActionResult ACRQuery(fileInput input)
        {
            var result = m.RunQuery(input);
            
            string url = string.Format("~/MusicFetch/Index?album={0}", result.album);
            return Redirect(url);
        }

        // GET: MusicFetch/Upload
        public ActionResult upload()
        {
            return View();
        }

        // GET: MusicFetch/Callback
        public ActionResult CallBack()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult AddFavourite(MusicFavouriteAdd music) {

            if(ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                if(userId == null)
                {
                    return Json(new { Status = 400,message = "User name not found, please login and retry" });
                }
                else {
                    music.UserId = userId;
                }
                
                if(favouriteFactory.addMusicFavourite(music))
                {
                    return Json(new { Status = 200 ,message = "["+music.MusicTitle+"] saved to your favourite list" });
                }
                else {
                    return Json(new { Status = 500,message = "[" + music.MusicTitle + "] already in your favourite list" });
                }
            }
            else {
                return Json(new { Status = 400,message = "Failed to save to favourite" });
            }
        }

    }
}
