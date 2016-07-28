using AutoMapper;
using DuckyData1._0._0Alpha;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACRCloud;
using static System.Net.WebRequestMethods;
using System.IO;
using System.Web.Mvc;
using DuckyData1._0._0Alpha.Factory.Account;
using System.Data.Entity.Validation;

namespace DuckyData1._0._0Alpha.Controllers
{
    public class MessageController : Controller
    {
        private Manager m = new Manager();
        private AccountFactory ac = new AccountFactory();
        static private string uID = System.Web.HttpContext.Current.User.Identity.GetUserId();

        // GET: Message
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(m.AllMsg());
        }

        // GET: Message
        [Authorize]
        public ActionResult Inbox()
        {
            var messageList = m.Inbox();

            return View(Mapper.Map<IEnumerable<MessageBase>>(messageList));
        }

        // GET: Message
        [Authorize]
        public ActionResult Sent()
        {
            return View(m.Outbox());
        }

        // GET: Message/Details/5
        [Authorize]

        public ActionResult Details(int id)
        {
            return View(m.GetMessageById(id));
        }


        // GET: Message/Details/5
        [Authorize]

        public ActionResult MarkUnread(int id)
        {
            m.MarkAsUnread(id);
            return RedirectToAction("Inbox", "Message");
        }


        // GET: Message/Reply
        [Authorize]
        public ActionResult Reply(int id)
        {
            var addForm = new MessageAddForm();
            var msg = m.GetMessageById(id);
            addForm.SentDate = DateTime.Now;
            addForm.Recipient = msg.UserName;
            addForm.Subject = string.Format("RE: {0}", msg.Subject);
            addForm.Body = string.Format("<br/><br/>--Original Message--\n{0}", msg.Body);
            return View(addForm);
        }

        // POST: Message/Reply
        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Reply(MessageAdd newItem)
        {
            if (ModelState.IsValid)
            {
                var addedItem = m.SendMessage(newItem);
                return RedirectToAction("Inbox", "Message");
            }
            return View();
        }

        // GET: Message/Create
        [Authorize]
        public ActionResult Create()
        {
            var addForm = new MessageAddForm();
            addForm.SentDate = DateTime.Now;
            return View(addForm);
        }

        // POST: Message/Create
        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(MessageAdd newItem)
        {
            var user = ac.findUserById(uID);
            if (user.gagged)
            {
                ModelState.AddModelError("SentDate", "Please select an option");
                return View();
            }
            if (ModelState.IsValid)
            {
                var addedItem = m.SendMessage(newItem);
                return RedirectToAction("Inbox", "Message");
            }
            return View();
        }

        // GET: Message/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue) { return HttpNotFound(); }

            var fetchedObject = m.GetMessageById(id.Value);
            
            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            //if (DateTime.Compare(fetchedObject.SentDate, DateTime.Now) > 300)
            
            return View(Mapper.Map<MessageEditForm>(fetchedObject));
            
        }

        // POST: Message/Edit/5
        [Authorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, MessageEdit newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {
                // Attempt to do the update
                var editedItem = m.EditMessage(newItem);
                if (editedItem == null)
                {
                    var editForm = AutoMapper.Mapper.Map<MessageEditForm>(newItem);

                    ModelState.AddModelError("modelState", "There was an error. (The edited data could not be saved.)");
                    return View(editForm);
                }
                else
                {
                    return RedirectToAction("details", new { id = editedItem.Id });
                }
            }
            else
            {
                var editForm = AutoMapper.Mapper.Map<MessageEditForm>(newItem);

                ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");

                return View(editForm);
            }
        
        }

        // GET: Message/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {

            if (!id.HasValue) { return HttpNotFound(); }

            // Attempt to fetch the object to be deleted
            var itemToDelete = m.GetMessageById(id.Value);

            if (itemToDelete == null)
            {
                return HttpNotFound();
                //return RedirectToAction("index");
            }
            else
            {
                var f = m.GetMessageById(itemToDelete.Id);
                return View(itemToDelete);
            }
        }

        // POST: Message/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (!id.HasValue) { return HttpNotFound(); }

            if( uID != m.GetMessageById(id.Value).UserId && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Inbox", "Message");
            }
            // Attempt to delete the item
            m.DeleteMessage(id.Value);

            return RedirectToAction("Inbox", "Message");
        }
    }
}
