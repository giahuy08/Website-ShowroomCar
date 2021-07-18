using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DoanWeb.Models;

namespace DoanWeb.Areas.Admin.Controllers
{
    
    public class ChartController : BaseController
    {
        // GET: Admin/Chart
      
        DoanWebEntities db = new DoanWebEntities();
        public ActionResult Index()
        {
           
            var list = db.Products.ToList();
           
            var category = list.Select(x => x.Category.nameCategory).Distinct();

            List<int> soluongFord = new List<int>();
            List<int> soluongKIA = new List<int>();

            foreach (var item in category)
            {
                soluongFord.Add(list.Count(x => x.Category.idCategory==1));
            }
            foreach (var item in category)
            {
                soluongKIA.Add(list.Count(x => x.Category.idCategory == 2));
            }
            ViewBag.category = category;
            ViewBag.soluongFord = soluongFord;
            ViewBag.soluongKIA = soluongKIA;


            //Pie

            //var listpdname = db.DetailOrders.Select(x => x.Product.nameProduct).ToList();
            //var listsp1 = db.DetailOrders.Where(x => x.idProduct == 1).Sum(s => s.Quantity);

            //ViewBag.slsp1 = listsp1;
            
            //var listsp2 = db.DetailOrders.Where(x => x.idProduct == 2).Sum(s => s.Quantity);
            // ViewBag.slsp2 = listsp2;

            //var listsp3 = db.DetailOrders.Where(x => x.idProduct == 3).Sum(s => s.Quantity);
            //ViewBag.slsp3 = listsp3;




            //var backgroundcolor = new List<string>();
            //var dataList = new List<int>();
            //foreach(var item in listpdname)
            //{

            //}


            return View();
        }
       
       
    }
}