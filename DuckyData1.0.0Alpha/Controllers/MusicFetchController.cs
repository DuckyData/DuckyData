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
    public class MusicFetchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager m = new Manager();
        
        public ActionResult Index()
        {
            return View();
        }

		
		
		

        public ActionResult Input()
        {
            var addForm = new fileInput();
            return View(addForm);
        }
        
        [HttpPost]
        public ActionResult Input(fileInput newItem)
        {
            if (ModelState.IsValid)
            {

                newItem.bytes = new byte[newItem.input.ContentLength];
                newItem.input.InputStream.Read(newItem.bytes, 0, newItem.input.ContentLength);
                //return RedirectToAction("ACRQuery", "MusicFetch", newItem);
                var result = m.RunQuery(newItem);
                var album = result.album;
                string tmp = string.Format("~/MusicFetch/Index?album={0}", album);
                
                return Redirect(tmp);
            }
            return View();
        }


        public ActionResult _MediaInput()
        {
            var addForm = new fileInput();
            return PartialView(addForm);
        }

        [HttpPost]
        public ActionResult _MediaInput(fileInput newItem)
        {

            if (ModelState.IsValid)
            {

                newItem.bytes = new byte[newItem.input.ContentLength];
                newItem.input.InputStream.Read(newItem.bytes, 0, newItem.input.ContentLength);
                var result = m.RunQuery(newItem);

                var album = result.album;
                string tmp = string.Format("~/MusicFetch/Index?album={0}", album);
                return Redirect(tmp);
            }
            return View();

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
    }
}
