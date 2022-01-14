using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GuestBook.Models;
using GuestBook.Encryptions;

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
            string user_ID = HttpContext.Session.GetString("userId");
            int user_id = int.Parse(user_ID);
            var guestBookContext = db.Messages.Include(n=>n.User).Where(m => m.UserId == user_id);
            ViewBag.code = Encryptor.EncryptData(user_ID, "enc123");
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
        public IActionResult Create(Message message,string code)
        {
            if (HttpContext.Session.GetString("userId") == null) { return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 401 }); }
            if(code == null)
            {
                ViewBag.requiredcode = "*";
                return View(message);
            }
            if (ModelState.IsValid)
            {
                message.UserId = int.Parse(Encryptor.DecryptData(code,"enc123"));
                message.MessageTime = DateTime.Now;
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
                    message.MessageTime = DateTime.Now;
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