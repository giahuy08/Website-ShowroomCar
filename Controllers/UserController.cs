using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DoanWeb.Models;

namespace DoanWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
       
        DoanWebEntities db = new DoanWebEntities();
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult SignUp(FormCollection f)
        {
            User_info us = new User_info();
            us.nameUser = f["name"];
            us.birthUser = DateTime.Parse(f["birth"]);
            us.sexUser = f["sex"];
            us.phoneNumberUser = f["phone"];
            us.emailUser = f["email"];
            us.addressUser = f["address"];
            us.roleAccount = 3;

            db.User_info.Add(us);

            Account acc = new Account();
            acc.iduser = us.idUser;
            acc.Pass = f["pass"];
            acc.Username= f["username"];
            db.Accounts.Add(acc);
            db.SaveChanges();
            return View();
        }

        public ActionResult SignIn()
        {
            ViewBag.Message = "Đăng nhập";
            ViewBag.Error = "";
             return View();
        }

        public ActionResult ChangeInfo (int? id)
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
                return RedirectToAction("ChangeInfo");
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
            [HttpPost]
        //xử lý đăng nhập
        public ActionResult SignIn(FormCollection f)
        {
       
            //string taikhoan = field["Username"].ToString();
            //string matkhau = f["Pass"].ToString();
            string taikhoan = f["Username"];
            string matkhau = f["Pass"];
           
            //string matkhaumd5 = GetMD5(matkhau);
            Account us = db.Accounts.SingleOrDefault(n => n.Username == taikhoan && n.Pass == matkhau);
            //nếu user nhập đúng mật khẩu
            if (us != null)
            {
                //if (us.block == false && us.usertype != "1")
                //{
                //    return Content("er_block");
                //}
                //else
                //{
                Session["user"] = us;
                //try
                //{ //lấy thời gian đăng nhập lưu vào hệ thống
                //    us.lastvisitdate = DateTime.Now;
                //    db.SaveChanges();
                //}
                //catch { };
                Session["ID"] = us.iduser;
                Session["UserAdmin"] =us.Username;
                Session["Name"] = us.User_info.nameUser;

                if (us.User_info.roleAccount == 1)
                {
                    Session["Role"] = "1";
                    Session["RoleName"] = "Manager";
                    return Content("/Admin/User_info/Index");
                }
                if (us.User_info.roleAccount == 2)
                {
                    Session["RoleName"] = "Staff";

                    Session["Role"] = "2";
                    return Content("/Admin/Products/Index");
                }
                if(us.User_info.roleAccount==3)
                {
                    Session["Role"] = "3";
                    return Content("/Home/Index");
                }
                      }
            ViewBag.Error = "Tai khoan khong ton tai";
            return Content("false");
        }
        public ActionResult SignOut()
        {
            Session["UserAdmin"] = "";
            Session["Name"]="";
            Session["Role"] = "";
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Dashboard()
        {
            return View();
        }
     


    }
}