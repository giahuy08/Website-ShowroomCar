using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoanWeb.Areas.Admin.Controllers
{
    public class BaseManagerController : Controller
    {
        // GET: Admin/BaseManager
        public BaseManagerController()
        {
            if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Home/Index");
            }

            if (System.Web.HttpContext.Current.Session["Role"].ToString() != "1")
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Home/Index");
            }
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }


        }
    }
}