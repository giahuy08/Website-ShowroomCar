using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoanWeb.Models;
//using PagedList;


namespace DoanWeb.Areas.Admin.Controllers
{
 
    public class ProductsController : BaseController
    {
        private DoanWebEntities db = new DoanWebEntities();

        // GET: Manager/Products
      
        //public ActionResult Index(int? page)
        //{
        //    if (page == null) page = 2;
        //    var links = (from l in db.Products
        //                 select l).OrderBy(x => x.idProduct);
        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);

        //    var products = db.Products.Include(p => p.Category);
        //    ViewBag.Products = products;
        //    return View(links.ToPagedList(pageNumber, pageSize));
        //}
        public ActionResult Index()
        {


            var products = db.Products.Include(p => p.Category);
            ViewBag.Products = products;
            return View(products.ToList());
        }
        // GET: Manager/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Manager/Products/Create
        public ActionResult Create()
        {
            ViewBag.idCategoryProduct = new SelectList(db.Categories, "idCategory", "nameCategory");
            return View();
        }

        // POST: Manager/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduct,idCategoryProduct,nameProduct,priceProduct,modelProduct,timeProduction,originProduct,descriptionProduct,color,seat,fuel")] Product product, HttpPostedFileBase image)
        {
            
            if (image != null && image.ContentLength > 0)
            {
                //product.imageProduct = new byte[image.ContentLength];
                //image.InputStream.Read(product.imageProduct, 0, image.ContentLength);
                string filename = System.IO.Path.GetFileName(image.FileName);
                string urlImage = Server.MapPath("~/Image/" + filename);
                image.SaveAs(urlImage);
                product.urlImageProduct = "Image/" + filename;

            }
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoryProduct = new SelectList(db.Categories, "idCategory", "nameCategory", product.idCategoryProduct);
            return View(product);
        }

        // GET: Manager/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoryProduct = new SelectList(db.Categories, "idCategory", "nameCategory", product.idCategoryProduct);
            return View(product);
        }

        // POST: Manager/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduct,idCategoryProduct,nameProduct,priceProduct,modelProduct,timeProduction,originProduct,descriptionProduct,color,seat,fuel")] Product product, HttpPostedFileBase editImage)
        {

            if (ModelState.IsValid)
            {
                Product modifyProduct = db.Products.Find(product.idProduct);
                //Product modifyProduct = db.Products.Where(p => p.idProduct == product.idProduct).FirstOrDefault();

                if (modifyProduct!= null)
                {
                    if (editImage != null && editImage.ContentLength > 0)
                    {
                        //product.imageProduct = new byte[editImage.ContentLength];
                        //editImage.InputStream.Read(modifyProduct.urlImageProduct,0, editImage.ContentLength);
                        string filename = System.IO.Path.GetFileName(editImage.FileName);
                        string urlImage = Server.MapPath("~/Image/" + filename);
                        editImage.SaveAs(urlImage);
                        product.urlImageProduct = "Image/" + filename;


                    }


                }
                db.Entry(modifyProduct).State = EntityState.Modified;
                //db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            //ViewBag.idCategoryProduct = new SelectList(db.Categories, "idCategory", "nameCategory", product.idCategoryProduct);
            return View(product);
        }
        //public ActionResult Edit([Bind(Include = "idProduct,idCategoryProduct,nameProduct,priceProduct,modelProduct,timeProduction,originProduct,descriptionProduct,urlImageProduct,color,seat,fuel")] Product product, HttpPostedFileBase editImage)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        //Product temp = db.Products.Find(product.idProduct);
        //        //Product temp = db.Products.Where(p => p.idProduct == product.idProduct).FirstOrDefault();
        //        //string url = temp.urlImageProduct;
        //        Product modifyProduct = product;
        //        if (modifyProduct != null)
        //        {
        //            if (editImage != null && editImage.ContentLength > 0)
        //            {
        //                //product.imageProduct = new byte[editImage.ContentLength];
        //                //editImage.InputStream.Read(modifyProduct.urlImageProduct,0, editImage.ContentLength);
        //                string filename = System.IO.Path.GetFileName(editImage.FileName);
        //                string urlImage = Server.MapPath("~/Image/" + filename);
        //                editImage.SaveAs(urlImage);
        //                product.urlImageProduct = "Image/" + filename;



        //            }



        //        }
        //        db.Entry(modifyProduct).State = EntityState.Modified;
        //        //db.Entry(product).State = EntityState.Modified;

        //        db.SaveChanges();
        //        return RedirectToAction("Index");

        //    }
        //    //ViewBag.idCategoryProduct = new SelectList(db.Categories, "idCategory", "nameCategory", product.idCategoryProduct);
        //    return View(product);
        //}

        // GET: Manager/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Manager/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
