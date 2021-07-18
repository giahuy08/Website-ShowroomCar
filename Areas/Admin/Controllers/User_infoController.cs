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
    public class User_infoController : BaseController
    {
        private DoanWebEntities db = new DoanWebEntities();

        // GET: Admin/User_info
        public ActionResult Index()
        {
            var user_info = db.User_info.Include(u => u.RoleAccount1);
            if (Session["RoleName"].ToString()=="Staff")
            {
                 user_info = db.User_info.Where(x=>x.RoleAccount1.idroleAccount==3);
            }
            else
            {
                user_info = db.User_info.Include(u => u.RoleAccount1);

            }
            //var user_info = db.User_info.Include(u => u.RoleAccount1);
            return View(user_info.ToList());
        }

        // GET: Admin/User_info/Details/5
        public ActionResult Details(int? id)
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
            return View(user_info);
        }

        // GET: Admin/User_info/Create
        public ActionResult Create()
        {
            ViewBag.roleAccount = new SelectList(db.RoleAccounts, "idroleAccount", "roleName");
            return View();
        }

        // POST: Admin/User_info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUser,nameUser,birthUser,sexUser,phoneNumberUser,emailUser,addressUser,roleAccount")] User_info user_info)
        {
            if (ModelState.IsValid)
            {
                db.User_info.Add(user_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.roleAccount = new SelectList(db.RoleAccounts, "idroleAccount", "roleName", user_info.roleAccount);
            return View(user_info);
        }

        // GET: Admin/User_info/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Admin/User_info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUser,nameUser,birthUser,sexUser,phoneNumberUser,emailUser,addressUser,roleAccount")] User_info user_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roleAccount = new SelectList(db.RoleAccounts, "idroleAccount", "roleName", user_info.roleAccount);
            return View(user_info);
        }

        // GET: Admin/User_info/Delete/5
        public ActionResult Delete(int? id)
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
            return View(user_info);
        }

        // POST: Admin/User_info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User_info user_info = db.User_info.Find(id);
            db.User_info.Remove(user_info);
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
