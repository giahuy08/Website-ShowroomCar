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
    public class ManageAccountController : BaseController
    {
        // GET: Admin/ManageAccount
        private DoanWebEntities db = new DoanWebEntities();


   
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult ChangeInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_info user_info = db.User_info.Find(id);
            if (user_info == null)
            {
                return HttpNotFound();
            }
            ViewBag.roleAccount = new SelectList(db.RoleAccounts, "idroleAccount", "roleName", user_info.roleAccount);
            return View(user_info);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInfo([Bind(Include = "idUser,nameUser,birthUser,sexUser,phoneNumberUser,emailUser,addressUser,roleAccount")] User_info user_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_info).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Thay đổi thành công", "success");

                return RedirectToAction("ChangeInfo");
            }
            else
            {
                SetAlert("Thay đổi thất bại", "success");
            }
            Session["ID"] = user_info.idUser;
            ViewBag.roleAccount = new SelectList(db.RoleAccounts, "idroleAccount", "roleName", user_info.roleAccount);
            return View(user_info);
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