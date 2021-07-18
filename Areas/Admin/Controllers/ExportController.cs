using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DoanWeb.Models;
using System.IO;

namespace DoanWeb.Areas.Admin.Controllers
{
    public class ExportController : BaseManagerController
    {

        public IList<ThongKe> GetThongKe()
        {
           DoanWebEntities db = new DoanWebEntities();
            var LaydataList = (from c in db.Categories
                                join p in db.Products
                                on c.idCategory equals p.idCategoryProduct
                                join d in db.DetailOrders on
                                p.idProduct equals d.idProduct

                                select new
                                {
                                    idhang = c.idCategory,
                                    tenhang = c.nameCategory,
                                    soluong = d.Quantity,
                                    tgtien = d.Price
                                }).ToList();
            List<ThongKe> tk = LaydataList.GroupBy(l => l.idhang).Select(
                cl => new ThongKe
                {
                    Hang = cl.Select(x=>x.tenhang).FirstOrDefault(),
                    Tongxe = (int)cl.Sum(x=>x.soluong),
                    DoanhThu = (double)cl.Sum(x=>x.tgtien)

                }).ToList();
            return tk;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View(this.GetThongKe());
        }

        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = this.GetThongKe();
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ThongKeDoanhThu.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return View("Index");

        }

    }
}