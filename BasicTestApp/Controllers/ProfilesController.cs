using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasicTestApp.Models;
using Microsoft.AspNet.Identity;

namespace BasicTestApp.Controllers
{
    [Authorize(Roles ="User")]
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profiles
        //public ActionResult Index()
        //{
        //    return View(db.Profiles.ToList());
        //}

        // GET: Profiles/Details/5
        public ActionResult Details()
        {
           
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             string   id = HttpContext.User.Identity.GetUserId();
            
            Profile profile = db.Profiles.Where(r=>r.UserID==id).FirstOrDefault();
            if (profile == null)
            {
                profile = new Profile { UserID = id };
            }
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
       // [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,FullName,Address,Gender,DOB,UserID")] Profile profile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Profiles.Add(profile);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(profile);
        //}

        // GET: Profiles/Edit/5
        [HttpGet]
        public ActionResult Edit(string id = "")
        {            
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Where(x=>x.UserID==id).FirstOrDefault();
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FullName,Address,Gender,DOB,UserID")] Profile paraProfile)
        {
            if (ModelState.IsValid)
            {
                string Userid = HttpContext.User.Identity.GetUserId();
                Profile profile = db.Profiles.Where(x => x.UserID == Userid).FirstOrDefault();
                profile.FullName = paraProfile.FullName;
                profile.Address = paraProfile.Address;
                profile.DOB = paraProfile.DOB;
                profile.Gender = paraProfile.Gender;

                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(paraProfile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
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
