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
using Microsoft.AspNet.Identity.EntityFramework;

namespace BasicTestApp.Areas.admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: admin/Profiles
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: admin/Profiles/Details/5
        public ActionResult Details(string id="")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser profile = db.Users.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }


        // GET: admin/Profiles/Edit/5
        public ActionResult Edit(string id="")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser profile = db.Users.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: admin/Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Address,Gender,DOB,LockoutEnabled")] ApplicationUser paraApplicationUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser=db.Users.Find(paraApplicationUser.Id);
                appUser.FullName = paraApplicationUser.FullName;
                appUser.Address = paraApplicationUser.Address;
                appUser.Gender = paraApplicationUser.Gender;
                appUser.DOB = paraApplicationUser.DOB;
                appUser.LockoutEnabled = paraApplicationUser.LockoutEnabled;
                
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paraApplicationUser);
        }

        // GET: admin/Profiles/Delete/5
        public ActionResult Delete(string id="")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser appUser= db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: admin/Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser profile = db.Users.Find(id);
            IdentityRole objRole=db.Roles.FirstOrDefault(x => x.Name == "Admin");
            if (profile.Roles.Any(x => x.RoleId == objRole.Id)) 
            {
                ModelState.AddModelError(string.Empty,"Admin cannot be deleted.");
                return View("Delete", profile);
            }
            db.Users.Remove(profile);
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
