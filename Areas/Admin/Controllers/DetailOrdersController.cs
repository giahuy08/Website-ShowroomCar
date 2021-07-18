using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoanWeb.Models;

namespace DoanWeb.Areas.Admin.Controllers
{
    public class DetailOrdersController : Controller
    {
        private DoanWebEntities db = new DoanWebEntities();

        // GET: Admin/DetailOrders
        public ActionResult Index()
        {
            var detailOrders = db.DetailOrders.Include(d => d.OrderProduct).Include(d => d.Product);
            return View(detailOrders.ToList());
        }

        // GET: Admin/DetailOrders/Details/5
        public ActionResult Details(int? id, int ?idpro)
        {
            if (id == null && idpro==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DetailOrder detailOrder = db.DetailOrders.Find(id,idpro);
            DetailOrder detailOrder = db.DetailOrders.Where(x => x.idOrder == id && x.idProduct == idpro).FirstOrDefault();
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

      
        // GET: Admin/DetailOrders/Edit/5
        public ActionResult Edit(int? id, int? idpro)
        {
            if (id == null || idpro ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = db.DetailOrders.Where(x => x.idOrder == id && x.idProduct == idpro).FirstOrDefault();
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.idOrder = new SelectList(db.OrderProducts, "idOrder", "Decription", detailOrder.idOrder);
            ViewBag.idProduct = new SelectList(db.Products, "idProduct", "nameProduct", detailOrder.idProduct);
            return View(detailOrder);
        }

        // POST: Admin/DetailOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduct,idOrder,Quantity,Price")] DetailOrder detailOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idOrder = new SelectList(db.OrderProducts, "idOrder", "Decription", detailOrder.idOrder);
            ViewBag.idProduct = new SelectList(db.Products, "idProduct", "nameProduct", detailOrder.idProduct);
            return View(detailOrder);
        }

        // GET: Admin/DetailOrders/Delete/5
        public ActionResult Delete(int? id, int? idpro)
        {
            if (id == null && idpro==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = db.DetailOrders.Where(x => x.idOrder == id && x.idProduct == idpro).FirstOrDefault();
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

        // POST: Admin/DetailOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idpro)
        {
            DetailOrder detailOrder = db.DetailOrders.Where(x => x.idOrder == id && x.idProduct == idpro).FirstOrDefault();
            db.DetailOrders.Remove(detailOrder);
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
