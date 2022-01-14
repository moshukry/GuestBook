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
                db.Add(reply);
                db.SaveChanges();
                return RedirectToAction("Index","Messages");
            }
            return View(reply);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await db.Replies
                .Include(r => r.Message)
                .FirstOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reply = await db.Replies.FindAsync(id);
            db.Replies.Remove(reply);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplyExists(int id)
        {
            return db.Replies.Any(e => e.ReplyId == id);
        }
    }
}
