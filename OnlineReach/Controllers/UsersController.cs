using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineReach.Models;

namespace OnlineReach.Controllers
{
//Never give up!
    public class UsersController : Controller
    {
        private OnlineMobileRechargeEntities db = new OnlineMobileRechargeEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Avatar).Include(u => u.Tune);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.AvatarID = new SelectList(db.Avatars, "AvatarID", "Link");
            ViewBag.TuneID = new SelectList(db.Tunes, "TuneID", "Link");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Email,Password,Phone,Fullname,Dob,AvatarID,TuneID,CreatedAt,UpdatedAt,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AvatarID = new SelectList(db.Avatars, "AvatarID", "Link", user.AvatarID);
            ViewBag.TuneID = new SelectList(db.Tunes, "TuneID", "Link", user.TuneID);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.AvatarID = new SelectList(db.Avatars, "AvatarID", "Link", user.AvatarID);
            ViewBag.TuneID = new SelectList(db.Tunes, "TuneID", "Link", user.TuneID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Email,Password,Phone,Fullname,Dob,AvatarID,TuneID,CreatedAt,UpdatedAt,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AvatarID = new SelectList(db.Avatars, "AvatarID", "Link", user.AvatarID);
            ViewBag.TuneID = new SelectList(db.Tunes, "TuneID", "Link", user.TuneID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
