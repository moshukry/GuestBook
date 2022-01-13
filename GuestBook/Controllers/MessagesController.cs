#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuestBook.Models;

namespace GuestBook.Controllers
{
    public class MessagesController : Controller
    {
        private readonly GuestBookContext db;
        //private string user_id;
        public MessagesController(GuestBookContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            //Authorize...
            if(HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 });
            }
            int user_id = int.Parse(HttpContext.Session.GetString("userId"));
            var guestBookContext = db.Messages.Include(n=>n.User).Where(m => m.UserId == user_id);
            return View(guestBookContext.ToList());
        }
        public IActionResult Details(int? id)
        {
            if (HttpContext.Session.GetString("userId") == null) { return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 }); }
            if (id == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }

            var message = db.Messages.Include(m => m.User).FirstOrDefault(m => m.MessageId == id);
            if (message == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }

            return View(message);
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userId") == null) { return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 }); }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Message message)
        {
            if (HttpContext.Session.GetString("userId") == null) { return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 }); }
            if (ModelState.IsValid)
            {
                message.UserId = int.Parse(HttpContext.Session.GetString("userId"));
                db.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }
        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetString("userId") == null) { return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 }); }
            if (id == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }

            var message = db.Messages.Find(id);
            if (message == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }
            return View(message);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Message message)
        {
            if (HttpContext.Session.GetString("userId") == null) { return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 }); }
            if (id != message.MessageId)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(message);
                    db.SaveChanges();
                }
                catch
                {
                   return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
                }
                return RedirectToAction("Index");
            }
            return View(message);
        }
        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("userId") == null) { return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 }); }
            if (id == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }
            var message = db.Messages.Include(m => m.User).FirstOrDefault(m => m.MessageId == id);
            if (message == null)
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index","Messages");
        }
    }
}