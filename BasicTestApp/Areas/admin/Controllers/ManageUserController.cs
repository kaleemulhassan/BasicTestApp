using BasicTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicTestApp.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: admin/ManageUser
        public ActionResult Index()
        {
            return View(db.Users.ToList());           
        }
    }
}