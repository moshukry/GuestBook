using GuestBook.Models;
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
            string id = Request.Cookies["id"];
            if (id != null)
            {
                HttpContext.Session.SetString("userId", id);
                ViewBag.looged = "";
                return RedirectToAction("Index", "Messages");
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
                HttpContext.Session.SetString("userId", u.UserId.ToString());
                ViewBag.looged = "";
                return RedirectToAction("Index", "Messages");
            }
            else
            {
                ViewBag.wrong = "Wrond Email or Password!!";
                return View("Login");
            }
        }
        public IActionResult LogOut()
        {
            //erase session...
            HttpContext.Session.Remove("userId");

            // erase cookies data...
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            option.Secure = true;
            option.IsEssential = true;
            Response.Cookies.Append("id", string.Empty, option);
            //Then delete the cookie...
            Response.Cookies.Delete("id");

            //Then redirect to login...
            return RedirectToAction("LogIn","Operations");
        }
    }
}
