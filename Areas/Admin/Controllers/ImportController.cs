using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DoanWeb.Models;
using LinqToExcel;
using System.Data.SqlClient;
using OfficeOpenXml;

namespace DoanWeb.Areas.Admin.Controllers
{
    public class ImportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            var productList = new List<Product>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var product = new Product();
                            product.idProduct = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            product.idCategoryProduct = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            product.nameProduct = workSheet.Cells[rowIterator, 2].Value.ToString();
                            product.priceProduct = Convert.ToDecimal(workSheet.Cells[rowIterator, 3].Value);
                            product.modelProduct = workSheet.Cells[rowIterator, 4].Value.ToString();
                            product.timeProduction = workSheet.Cells[rowIterator, 5].Value.ToString();
                            product.originProduct = workSheet.Cells[rowIterator, 6].Value.ToString();
                            product.descriptionProduct = workSheet.Cells[rowIterator, 7].Value.ToString();
                            product.urlImageProduct = workSheet.Cells[rowIterator, 8].Value.ToString();
                            product.color = workSheet.Cells[rowIterator, 9].Value.ToString();
                            product.seat = Convert.ToInt32(workSheet.Cells[rowIterator, 10].Value);
                            product.fuel = workSheet.Cells[rowIterator, 11].Value.ToString();







                            productList.Add(product);
                        }
                    }
                }
            }
            using (DoanWebEntities db = new DoanWebEntities())
            {
                foreach (var item in productList)
                {
                    db.Products.Add(item);
                }
                db.SaveChanges();
            }
            return View("Index");
        }



    }
}