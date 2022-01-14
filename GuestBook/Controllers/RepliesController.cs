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
    public class RepliesController : Controller
    {
        private readonly GuestBookContext db;

        public RepliesController(GuestBookContext context)
        {
            db = context;
        }
        public IActionResult Create(int id)
        {
            Reply reply = new Reply();
            reply.MessageId = id;
            return View(reply);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reply reply)
        {
            if (ModelState.IsValid)
            {
                reply.ReplyTime =DateTime.Now;
                db.Add(reply);
                db.SaveChanges();
                return RedirectToAction("Index","Messages");
            }
            return View(reply);
        }
    }
}
