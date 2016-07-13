using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class ImageController : Controller
    {
        private Manager m = new Manager();

        [Route("image/{id}")]
        public ActionResult GetFile(int? id)
        {
            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Fetch the object, so that we can inspect its value
            var fetchedObject = m.GetMessageById(id.GetValueOrDefault());

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(fetchedObject.Attachment, fetchedObject.ContentType, fetchedObject.ContentName);
            }
        }
    }
}