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
using System.Configuration;

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
        
       
        public FileResult Download(string file, string fileName)
        {
            //var file = Server.HtmlEncode(url);
            return File(file, "application/octet-stream",fileName);
        }


        public ActionResult _MediaInput()
        {
            var addForm = new fileInput();
            return PartialView(addForm);
        }
        
        [HttpPost]
        public JsonResult _MediaInput(int? id, fileInput newItem)
        {
            if (ModelState.IsValid)
            {
                MediaResponse response = new MediaResponse();
                newItem.bytes = new byte[newItem.input.ContentLength];
                newItem.input.InputStream.Read(newItem.bytes, 0, newItem.input.ContentLength);

                var ext = Path.GetExtension(newItem.input.FileName);
                var check = new byte[] {
                    newItem.bytes[0],
                    newItem.bytes[1],
                    newItem.bytes[2],
                    newItem.bytes[3],
                    newItem.bytes[4],
                    newItem.bytes[5],
                    newItem.bytes[6],
                    newItem.bytes[7],
                    newItem.bytes[8],
                    newItem.bytes[9],
                    };
                //mp3 mime check
                if (ext.ToLower() == ".mp3")
                {
                    
                    var mp3 = new byte[] { 73, 68, 51 };
                    var mp3a = new byte[] { 255, 251 };
                    if ((check[0] != mp3[0] || check[1] != mp3[1] || check[2] != mp3[2]) &&
                        (check[0] != mp3a[0] || check[1] != mp3a[1]))
                    {
                        return Json(new { mimeStatusCode = false, statusCode = 400, msg="MP3 file not correctly formatted"});
                    }
                }
                //mp3 mime check end

                //flac mime check
                if (ext.ToLower() == ".flac")
                {
                    
                    var flac = new byte[] { 102, 76, 97, 67 };
                    if (check[0] != flac[0] || check[1] != flac[1] || check[2] != flac[2] || check[3] != flac[3])
                    {
                        return Json(new { mimeStatusCode = false,statusCode = 400,msg = "FLAC file not correctly formatted" });
                    }
                }
                //flac mime check end

                //wma mime check
                if (ext.ToLower() == ".wma")
                {

                    var wma = new byte[] { 142, 102, 207, 17, 166, 217, 0, 170, 0, };
                    if (check[0] != wma[0] || check[1] != wma[1] || check[2] != wma[2] || check[3] != wma[3]
                        || check[4] != wma[4] || check[5] != wma[5] || check[6] != wma[6] || check[7] != wma[7]
                        || check[8] != wma[8])
                    {
                        return Json(new { mimeStatusCode = false,statusCode = 400,msg = "WMA file not correctly formatted" });
                    }
                }
                //wma mime check end

                //mp4 mime check
                if (ext.ToLower() == ".mp4")
                {

                    var mp4 = new byte[] { 0, 0, 0, 24, 102, 116, 121, 112, 109, 112 };
                    if (check[0] != mp4[0] || check[1] != mp4[1] || check[2] != mp4[2] || check[3] != mp4[3]
                        || check[4] != mp4[4] || check[5] != mp4[5] || check[6] != mp4[6] || check[7] != mp4[7]
                        || check[8] != mp4[8] || check[9] != mp4[9])
                    {
                        return Json(new { mimeStatusCode = false,statusCode = 400,msg = "MP4 file not correctly formatted" });
                    }
                }
                //mp4 mime check end

                //m4a mime check
                if (ext.ToLower() == ".m4a")
                {

                    var m4a = new byte[] { 0, 0, 0, 32, 102, 116, 121, 112, 77, 52, 65 };
                    if (check[0] != m4a[0] || check[1] != m4a[1] || check[2] != m4a[2] || check[3] != m4a[3]
                        || check[4] != m4a[4] || check[5] != m4a[5] || check[6] != m4a[6] || check[7] != m4a[7]
                        || check[8] != m4a[8] || check[9] != m4a[9] || check[10] != m4a[10])
                    {
                        return Json(new { mimeStatusCode = false,statusCode = 400,msg = "M4A file not correctly formatted" });
                    }
                }
                //m4a mime check end
                response.mimeStatusCode = true;
                
                var result = m.RunQuery(newItem);
                string qry;
                
                if (result.album != null && result.artists[0] != null && result.title != null)
                {
                    response.statusCode = true;
                }
                response.fileURL = result.path;
                response.fileName = result.title + ext;


                if (newItem.input.ContentType.Contains("audio"))
                {
                    qry = result.album;
                    response.queryURL = string.Format("MusicFetch/Index?album={0}", qry);
                    return Json(new {
                        mimeStatusCode = true,
                        statusCode = 200,
                        qry = result.album,
                        queryURL = string.Format("MusicFetch/Index?album={0}", qry),
                        fileURL = result.path,
                        fileName = result.title + ext,

                        album = result.album,
                        artURL = result.artURL,
                        artists = result.artists,
                        genres = result.genres,
                        producer = result.producer,
                        releaseDate = result.releaseDate,
                        title = result.title
                    });
                }
                else if (newItem.input.ContentType.Contains("video"))
                {
                    qry = result.title;
                    response.queryURL = string.Format("VideoFetch/Index?video={0}", qry);
                    return Json(new {
                        mimeStatusCode = true,
                        statusCode = 200,
                        qry = result.title,
                        queryURL = string.Format("VideoFetch/Index?video={0}", qry),
                        fileURL = result.path, fileName = result.title + ext,

                        album = result.album,
                        artURL = result.artURL,
                        artists = result.artists,
                        genres = result.genres,
                        producer = result.producer,
                        releaseDate = result.releaseDate,
                        title = result.title
                    });
                }
            }
            return Json(new { statusCode=400,msg="Data not valid" });

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
