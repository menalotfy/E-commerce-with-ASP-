using Souq.Models;
using Souq.Models.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Souq.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ProductController : Controller
    {
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index(int? Page)
        {

            Product product = new Product();
            return View(applicationDbContext.Product.Where(p => p.Status == false).ToList().ToPagedList(Page ?? 1, 4));
        }


        //[ChildActionOnly]
        public ActionResult Search(string ProductName, int? Page)
        {
            var Pro = applicationDbContext.Product.Where(p => p.ProductName.Contains(ProductName) && p.Status == false).ToList().ToPagedList(Page ?? 1, 4);
            //var Pro = (from p in EcommerceEntities.Products
            //           where p.ProductName.Contains(ProductName) && p.Status == false
            //           select p).ToList().ToPagedList(Page ?? 1, 4);
            return View("Index", Pro);
        }

        //[ChildActionOnly]
        //public ActionResult SearchProduct(string ProductName)
        //{
        //    var Pro = (from p in applicationDbContext.Product
        //               where p.ProductName.Contains(ProductName) && p.Status == false
        //               select p).ToList();
        //    return PartialView("_SearchProductName", Pro);
        //}

        //[ChildActionOnly]
        //public ActionResult Details(int id)
        //{
        //    return View(EcommerceEntities.Products.FirstOrDefault(a => a.ID == id));
        //}


        //ajax
        //[ChildActionOnly]
        public ActionResult ProductDetails(int id)
        {
            var ProDetails = applicationDbContext.Product.FirstOrDefault(a => a.ID == id);
            return PartialView("_ProductDetails", ProDetails);
        }
        [HttpGet]
        public ActionResult ViewCart(int id)
        {
            var q = applicationDbContext.Product.FirstOrDefault(p => p.ID == id);
            return View();

        }
    }
}