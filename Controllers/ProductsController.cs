using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoanWeb.Models;
using DoanWeb.Models.ViewModels;
using PagedList;




namespace DoanWeb.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        DoanWebEntities db = new DoanWebEntities();
        public ActionResult ListProduct(int? page,string SearchName, int Category =0)
        {
            
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var categories = from c in db.Categories select c;
            ViewBag.Category = new SelectList(categories, "idCategory", "nameCategory"); // danh sách Category

            var links = db.Products.ToList();
            if (!String.IsNullOrEmpty(SearchName))
            {
                 links = db.Products.Where(x => x.nameProduct.Contains(SearchName) || SearchName == null).ToList();

            }
            if(Category!=0)
            {
                links = links.Where(x => x.idCategoryProduct == Category).ToList();
            }
          

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));

         

        }
        public ActionResult ListCategory()
        {
            var list = db.Categories.Select(m => m.nameCategory).ToList();
            return View("_ListCategory",list);
;
        }
       

    
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
            ViewBag.other = db.Products.ToList();
            return View(product);
        }
        

        //So sánh

        public Compare GetCompare()
        {
            Compare item = Session["Compare"] as Compare;
            if (item == null || Session["Compare"] == null)
            {
                item = new Compare();
                Session["Compare"] = item;
            }
            return item;
        }

        public ActionResult AddtoCompare(int id)
        {
            var pro = db.Products.SingleOrDefault(s => s.idProduct == id);
            if (pro != null)
            {
                GetCompare().Add(pro);
            }

            return RedirectToAction("Compare", "Products");
        }
        //trang
       

        public ActionResult Compare()
        {
            if (Session["Compare"] == null)
                return RedirectToAction("Compare", "Products");
            Compare compare = Session["Compare"] as Compare;
            return View(compare);



          

        }
        public ActionResult DeleteCompare(int id)
        {
            Compare compare = Session["Compare"] as Compare;
            compare.Delete(id);
            return RedirectToAction("Compare", "Products");

        }
      
        public ActionResult ShowCompare()
        {
            if (Session["Compare"] == null)
                return RedirectToAction("ShowCompare", "ShowCompare");
            Compare compare = Session["Compare"] as Compare;
            return View(compare);
        }

    }
}