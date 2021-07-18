using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoanWeb.Models;
namespace DoanWeb.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        DoanWebEntities db = new DoanWebEntities();
        public Order GetOrder()
        {
            Order item = Session["Order"] as Order;
            if (item == null || Session["Order"] == null)
            {
                item = new Order();
                Session["Order"] = item;
            }
            return item;
        }
        public ActionResult AddtoOrder(int id)
        {
            var pro = db.Products.SingleOrDefault(s => s.idProduct == id);
            if (pro != null)
            {
                GetOrder().Add(pro);
            }

            return RedirectToAction("ShowOrder", "Order");
        }
        //trang
        public ActionResult ShowOrder()
        {
            if (Session["Order"] == null)
                return RedirectToAction("ShowOrder", "Order");
            Order order = Session["Order"] as Order;
            return View(order);
        }
        public ActionResult Update_Quantity(FormCollection form)
        {
            Order order = Session["Order"] as Order;
            int id_pro = int.Parse(form["id_product"]);
            int quantity = int.Parse(form["Quantity"]);
            order.Update_quantity(id_pro, quantity);
            return RedirectToAction("ShowOrder", "Order");
        }
        public ActionResult DeleteOrder(int id)
        {
            Order order = Session["Order"] as Order;
            order.Delete(id);
            return RedirectToAction("ShowOrder", "Order");

        }
        public ActionResult Order(int ?id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProduct order = db.OrderProducts.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }


            return View(order);


        }
       

        public ActionResult Order_success()
        {
            return View();
        }
        //Chekout
        public ActionResult Checkout (FormCollection form)
        {
            try
            {
                Order order = Session["Order"] as Order;
                OrderProduct orderproduct = new OrderProduct();
                orderproduct.createdDate = DateTime.Parse(form["Date"]);
                //orderproduct.idCustomer = int.Parse(form["CodeCus"]);
                orderproduct.idCustomer = ((int?)Session["ID"]);
                orderproduct.Decription = form["Address"];
                db.OrderProducts.Add(orderproduct);

                foreach(var item in order.Items)
                {
                    DetailOrder detail = new DetailOrder();
                    detail.idOrder = orderproduct.idOrder;
                    detail.idProduct = item.product.idProduct;
                    detail.Price = item.product.priceProduct*item.quantity;
                    detail.Quantity = item.quantity;
                    db.DetailOrders.Add(detail);
                }
                db.SaveChanges();
                order.ClearOrder();
                return RedirectToAction("Order_success","Order");
            }
            catch
            {
                return Content("Error Order. Please infomation of Customer");
            }
        }
        public ActionResult InformationCustomer(int id)
        {

            Account account = db.Accounts.FirstOrDefault(x=>x.iduser ==id);
            ViewBag.account = account;
            User_info userinfo= db.User_info.FirstOrDefault(a => a.idUser == id);
            ViewBag.infoCus = userinfo;
            var kt = db.OrderProducts.Where(x => x.idOrder == id).FirstOrDefault();
            if (kt != null)
            {
                OrderProduct order = db.OrderProducts.FirstOrDefault(a => a.idCustomer == id);
                ViewBag.order = order;
                DetailOrder orderdetail = db.DetailOrders.FirstOrDefault(a => a.idOrder == order.idOrder);
                ViewBag.orderdetail = orderdetail;
                Product proorder = db.Products.FirstOrDefault(a => a.idProduct == orderdetail.idProduct);
                ViewBag.product = proorder;
            }



            return View();     
            
        }

    }
}