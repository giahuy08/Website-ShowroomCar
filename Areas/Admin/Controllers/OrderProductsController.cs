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
    public class OrderProductsController : Controller
    {
        // GET: Admin/OrderProducts
        private DoanWebEntities db = new DoanWebEntities();
        public ActionResult Index()
        {
            var listorder = db.OrderProducts.ToList();
            return View(listorder);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //OrderProduct Order = db.OrderProducts.Where(x => x.idOrder == id ).FirstOrDefault();
            OrderProduct order = db.OrderProducts.Find(id);
            if ( order== null)
            {
                return HttpNotFound();
            }
       

            ViewBag.idCustomer = new SelectList(db.User_info, "idUser", "nameUser", order.idCustomer);
            //ViewBag.idProduct = new SelectList(db.Products, "idProduct", "nameProduct", Order.idProduct);
            return View(order);
        }

        // POST: Admin/DetailOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOrder,idCustomer,createdDate,Decription")] OrderProduct order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCustomer = new SelectList(db.User_info, "idCustomer", "nameUser", order.idCustomer);

            return View(order);
        }






        public ActionResult Delete(int? idOrder)
        {
            if (idOrder == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct order = db.OrderProducts.Find(idOrder);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Manager/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idOrder)
        {
            OrderProduct order = db.OrderProducts.Find(idOrder);
            db.OrderProducts.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}