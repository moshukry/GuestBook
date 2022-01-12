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
        private int user_id;

        public MessagesController(GuestBookContext db)
        {
            this.db = db;
            user_id = int.Parse(HttpContext.Session.GetString("userId"));
        }
        public IActionResult Index()
        {
            var guestBookContext = db.Messages.Where(m => m.UserId == user_id);
            return View(guestBookContext.ToList());
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = db.Messages.Include(m => m.User).FirstOrDefault(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Password");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Add(message);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Password", message.UserId);
            return View(message);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
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
                   return NotFound();
                }
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Password", message.UserId);
            return View(message);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = db.Messages.Include(m => m.User).FirstOrDefault(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }
    }
}
