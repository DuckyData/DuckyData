using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels.MusicFetch;
using DuckyData1._0._0Alpha.Factory.FavouriteFactory;
using System.Drawing;
using System.IO;

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
        public MediaResponse _MediaInput(int? id, fileInput newItem)
        {
            if (ModelState.IsValid)
            {
                MediaResponse response = new MediaResponse();
                newItem.bytes = new byte[newItem.input.ContentLength];
                






                newItem.input.InputStream.Read(newItem.bytes, 0, newItem.input.ContentLength);
                var result = m.RunQuery(newItem);

                /*
                Response.Buffer = false;
                Response.Clear();
                Response.ContentType = newItem.input.ContentType;
                var ext = Path.GetExtension(newItem.input.FileName);
                Response.AddHeader("content-disposition", "attachment; filename=" + result.title + ext);
                Response.AddHeader("content-length", newItem.input.ContentLength.ToString());
                Response.TransmitFile(result.path);
                Response.End();
                Response.Flush();
                */
                string qry;
                if(result.album != null && result.artists[0] != null && result.title != null )
                {
                    response.statusCode = true;
                }
                response.fileURL = result.path;
                if (id.Value == 2)
                {
                    if (newItem.input.ContentType.Contains("audio"))
                    {
                        qry = result.album;
                        response.queryURL = string.Format("MusicFetch/Index?album={0}", qry);
                    }
                    else if (newItem.input.ContentType.Contains("video"))
                    {
                        qry = result.title;
                        response.queryURL = string.Format("VideoFetch/Index?video={0}", qry);
                    }
                }
                return response;
            }
            return new MediaResponse();

        }

        [Authorize]
        public ActionResult ACRQuery(fileInput input)
        {
            
            var result = m.RunQuery(input);
            //return RedirectToAction("Index", "MusicFetch", new { id = "?album=" + result.album });
            
            string url = string.Format("~/MusicFetch/Index?album={0}", result.album);
            return Redirect(url);
        }


		
		
        // GET: MusicFetch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaFile mediaFile = db.MediaFiles.Find(id);
            if (mediaFile == null)
            {
                return HttpNotFound();
            }
            return View(mediaFile);
        }

        // GET: MusicFetch/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusicFetch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fileName,mediaType,fileData")] MediaFile mediaFile)
        {
            if (ModelState.IsValid)
            {
                db.MediaFiles.Add(mediaFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mediaFile);
        }

        // GET: MusicFetch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaFile mediaFile = db.MediaFiles.Find(id);
            if (mediaFile == null)
            {
                return HttpNotFound();
            }
            return View(mediaFile);
        }

        // POST: MusicFetch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fileName,mediaType,fileData")] MediaFile mediaFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mediaFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediaFile);
        }

        // GET: MusicFetch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MediaFile mediaFile = db.MediaFiles.Find(id);
            if (mediaFile == null)
            {
                return HttpNotFound();
            }
            return View(mediaFile);
        }

        // POST: MusicFetch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MediaFile mediaFile = db.MediaFiles.Find(id);
            db.MediaFiles.Remove(mediaFile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: MusicFetch/Upload
        public ActionResult upload()
        {
            return View();
        }


        // GET: MusicFetch/Upload
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
