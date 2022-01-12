﻿using GuestBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers
{
    public class OperationsController : Controller
    {
        private readonly GuestBookContext db;

        public OperationsController(GuestBookContext db)
        {
            this.db = db;
        }
        public IActionResult LogIn()
        {
            string id = Request.Cookies["userId"];
            if (id != null)
            {
                HttpContext.Session.SetString("UserId", id);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(User user,bool remember)
        {
            User u = db.Users.Where(n=>n.UserEmail == user.UserEmail && n.Password == user.Password).FirstOrDefault();
            if (u != null)
            {
                if (remember)
                {
                    CookieOptions opt = new CookieOptions();
                    opt.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Append("id", u.UserId.ToString(), opt);
                }
                HttpContext.Session.SetString("UserId", u.UserId.ToString());
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.wrong = "Wrond Email or Password!!";
            }
            return RedirectToAction("LogIn");
        }
    }
}