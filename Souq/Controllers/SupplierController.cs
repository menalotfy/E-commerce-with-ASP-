using PagedList;
using PagedList.Mvc;
using Souq.Models;
using Souq.Models.MyClasses;
using Souq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Souq.Controllers
{
    [Authorize(Roles ="Supplier")]
    public class SupplierController : Controller
    {
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();


        public ActionResult CompareProductPriceDiscount(Product product)
        {
            if (product.ProductDiscount < product.ProductPrice)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Index(int? Page)
        {
            var id = User.Identity.GetUserId();
            var query = (from p in applicationDbContext.Product
                         where (p.Status == false && p.Supplier_Id == id)
                         select p).ToList().ToPagedList(Page ?? 1, 5);
            return View("Index", query);
        }


        // [ChildActionOnly]
        public ActionResult Search(string ProductName, int? Page)
        {
            var id = User.Identity.GetUserId();

            var Pro = (from p in applicationDbContext.Product
                       where p.ProductName.Contains(ProductName) && p.Status == false && p.Supplier_Id==id
                       select p).ToList().ToPagedList(Page ?? 1, 4);

            return View("Index", Pro);
        }


        [HttpGet]
        public ActionResult EditVM(int id)
        {
            Product pro = applicationDbContext.Product.FirstOrDefault(c => c.ID == id);
            ProductMetaDataVM provm = new ProductMetaDataVM();
            provm.ID = pro.ID;
            provm.ProductPrice = pro.ProductPrice;
            provm.ProductQuantity = pro.ProductQuantity;
            provm.ProductImage = pro.ProductImage;
            provm.ProductDiscount = pro.ProductDiscount;
            provm.Category_ID = pro.Category_Id;
            provm.ProductName = pro.ProductName;
            provm.categories= applicationDbContext.Categories.ToList();

            return View(provm);
        }

        [HttpPost]
        public ActionResult EditVM(int id, Product Pro)
        {
            if (Pro.ProductName != null &&
                Pro.ProductPrice > 0 &&
                Pro.Category_Id != 0 &&
                Pro.ProductQuantity >= 0 &&
                Pro.ProductDiscount >= 0 &&
                Pro.ProductPrice > Pro.ProductDiscount
                )
            {

                if (ModelState.IsValid)
                {
                    Product oldPro = applicationDbContext.Product.FirstOrDefault(d => d.ID == id);
                    oldPro.ProductName = Pro.ProductName;
                    oldPro.ProductPrice = Pro.ProductPrice;
                    oldPro.ProductQuantity = Pro.ProductQuantity;
                    oldPro.ProductDiscount = Pro.ProductDiscount;
                    oldPro.Category_Id = Pro.Category_Id;

                    HttpPostedFileBase image = Request.Files["image"];
                    if (image.FileName != "")
                    {
                        image.SaveAs(Server.MapPath("~/Images/") + System.IO.Path.GetFileName(image.FileName));
                        oldPro.ProductImage = image.FileName;
                    }
                    applicationDbContext.SaveChanges();
                    return RedirectToAction("Index");
                }

                else
                {
                    return View(Pro);
                }
            }
            else
            {
                return View(Pro);
            }
        }

        

        [HttpGet]
        public ActionResult Add()
        {
            var cate = applicationDbContext.Categories.ToList();
            ProductMetaDataVM pro = new ProductMetaDataVM();
            foreach (var item in cate)
            {
                pro.categories.Add(item);
            }
            return View(pro);
        }



        [HttpPost]
        public ActionResult Add(Product Pro)
        {
            Product product = applicationDbContext.Product.FirstOrDefault(p => p.ProductName == Pro.ProductName);

            if (product != null)
            {
                ModelState.AddModelError("", "Product Name already exists, please change it");
                return View(Pro);
            }
            else
            {
                if (Pro.ProductName != null &&
                   Pro.ProductPrice >= 1 &&
                   Pro.ProductQuantity >= 0 &&
                   Pro.ProductDiscount >= 0  )
                   
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            HttpPostedFileBase image = Request.Files["image"];
                            image.SaveAs(Server.MapPath("~/Images/") + System.IO.Path.GetFileName(image.FileName));
                            Pro.ProductImage = image.FileName;
                            //string file_name = Pro.ProductImage + Path.GetExtension(image.FileName);

                            //string path = Path.Combine(Server.MapPath("~/Images"), file_name);
                            //image.SaveAs(path);


                            var id = User.Identity.GetUserId();
                            Pro.Status = false;
                            Pro.Supplier_Id = id;
                            //  Pro.ProductImage = file_name;
                            applicationDbContext.Product.Add(Pro);
                            applicationDbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        catch
                        {
                            return View(Pro);
                        }
                    }
                    else
                    {
                        return View(Pro);
                    }
                }
                else
                {
                    return View(Pro);
                }
            }
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

        ////////////////////////////// work with VM /////////////////////////////////

        //[HttpGet]
        //public ActionResult Create()
        //{
           

        //    ViewBag.Category = new SelectList(applicationDbContext.Categories, "ID", "CategoryName");
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult Create(ProductMetaDataVM Pro)
        //{
        //    Product product = applicationDbContext.Product.FirstOrDefault(p => p.ProductName == Pro.ProductName);
        //    if (product != null)
        //    {
        //        ModelState.AddModelError("", "Product Name already exists, please change it");
        //        return View(Pro);
        //    }
        //    else
        //    {
        //        if (Pro.ProductName != null &&
        //           Pro.ProductPrice >= 1 &&
        //           Pro.ProductQuantity >= 0 &&
        //           Pro.ProductDiscount >= 0 &&
        //           Pro.ProductPrice > Pro.ProductDiscount
        //          )
        //        {

        //            if (ModelState.IsValid)
        //            {
        //                Product prooduct = new Product();
        //                prooduct.ProductName = Pro.ProductName;
        //                prooduct.ProductPrice = Pro.ProductPrice;
        //                prooduct.ProductQuantity = Pro.ProductQuantity;
        //                prooduct.ProductDiscount = Pro.ProductDiscount;
        //               // prooduct.productSuppliers = Pro.ProductSuppliers;
        //                //prooduct.ProductImage = Pro.ProductImage;
        //                //prooduct.Category.CategoryName = Pro.Category.CategoryName;
        //                //prooduct.Category.CategoryDescription = Pro.Category.CategoryDescription;
        //                //prooduct.Category_ID = Pro.Category_ID;


        //                //List<ProductMetaDataVM> ProductMetaDataVMList = new List<ProductMetaDataVM>();
        //                //List<Product> ProductList = ecommerceEntities.Products.ToList();
        //                //foreach (var item in ProductList)
        //                //{
        //                //    ProductMetaDataVM productMetaDataVMObj = new ProductMetaDataVM();
        //                //    productMetaDataVMObj.Category.CategoryName = item.Category.CategoryName;
        //                //    foreach (var pro in item.ProductSuppliers)
        //                //    {
        //                //        productMetaDataVMObj.ProductSuppliers.Add(pro);
        //                //    }
        //                //    ProductMetaDataVMList.Add(productMetaDataVMObj);
        //                //}


        //                //ProductMetaDataVMList.Add(Pro);
        //                //Pro.Status = false;
        //                //ecommerceEntities.Products.Add(prooduct);
        //                applicationDbContext.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                return View(Pro);
        //            }
        //        }
        //        else
        //        {
        //            return View(Pro);
        //        }
        //    }
        //}

    }
}