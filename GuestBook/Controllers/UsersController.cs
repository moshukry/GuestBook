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
    public class UsersController : Controller
    {
        private readonly GuestBookContext db;

        public UsersController(GuestBookContext context)
        {
            db = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index","Messages");
            }
            return View(user);
        }
    }
}
