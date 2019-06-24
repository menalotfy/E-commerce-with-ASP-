using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Souq.Models;
using Souq.Models.MyClasses;

namespace Souq.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminSupplierController : Controller
    {
      
        private ApplicationDbContext applicationDbContext = new ApplicationDbContext();


        public ActionResult Search(string SupplierName, int? Page)
        {
            var cust = (from p in applicationDbContext.Users
                        where (p.Roles.Any(r => r.RoleId == "2") && p.UserName.Contains(SupplierName) && p.Status == false)
                        select p).ToList().ToPagedList(Page ?? 1, 4);

            return View("Index", cust);
        }


        public ActionResult Index(int? Page)
        {
            var customer = (from u in applicationDbContext.Users
                            where (u.Roles.Any(r => r.RoleId == "2") && u.Status == false)
                            select u).ToList().ToPagedList(Page ?? 1, 5);
            return View(customer);
        }


        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = applicationDbContext.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = applicationDbContext.Users.Find(id);
            applicationUser.Status = true;
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
