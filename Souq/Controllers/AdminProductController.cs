using PagedList;
using Souq.Models;
using Souq.Models.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Souq.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {

        ApplicationDbContext applicationDbContext = new ApplicationDbContext();

        public ActionResult Search(string ProductName, int? Page)
        {
            var Pro = (from p in applicationDbContext.Product
                       where p.ProductName.Contains(ProductName) && p.Status == false
                       select p).ToList().ToPagedList(Page ?? 1, 4);

            return View("Index", Pro);
        }

        public ActionResult Index(int? Page)
        {
            return View("Index", applicationDbContext.Product.Where(p => p.Status == false).ToList().ToPagedList(Page ?? 1, 5));
        }


        public ActionResult Delete(int id)
        {
            return View(applicationDbContext.Product.FirstOrDefault(a => a.ID == id));
        }


        [HttpPost]
        public ActionResult Delete(Product delPro, int id)
        {
            if (delPro.Status == false)
            {
                Product del = applicationDbContext.Product.FirstOrDefault(d => d.ID == id);
                del.Status = true;
                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(delPro);
            }
        }
    }
}