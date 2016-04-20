using homework1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace homework1.Controllers
{
    public class AccountController : BaseController
    {
        string UserData = "";

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel data)
        {
            // 登入時清空所有 Session 資料
            Session.RemoveAll();            

            if (CheckLogin(data))
            {
                FormsAuthentication.RedirectFromLoginPage(data.Account, false);

                // 將管理者登入的 Cookie 設定成 Session Cookie
                bool isPersistent = false;                

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                  data.Account,
                  DateTime.Now,
                  DateTime.Now.AddMinutes(30),
                  isPersistent,
                  UserData,
                  FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));                
                
                return RedirectToAction("Index", "Home");                
            }

            return View();
        }

        private bool CheckLogin(LoginViewModel data)
        {            
            if (data.Account == "admin" && data.Password == "123456")
            {
                UserData = "gold_member,board_admin";
                return true;
            }
            else
            {
                var 客戶資料 = repo客戶資料.GetByAccount(data.Account);
                bool IsAllowLogin = (客戶資料 != null);
                UserData = "gold_member";
                return IsAllowLogin;
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "gold_member")]
        public ActionResult EditProfile()
        {
            var 客戶資料 = repo客戶資料.GetByAccount(User.Identity.Name);

            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }

        [Authorize(Roles = "gold_member")]
        [HttpPost]
        public ActionResult EditProfile(FormCollection form)
        {
            var 客戶資料 = repo客戶資料.Find(Convert.ToInt32(form["id"]));            

            if (客戶資料.Password != form["Password"])
            {
                客戶資料.Password = repo客戶資料.HashPassword(User.Identity.Name, form["Password"]);
            }

            if (TryUpdateModel(客戶資料, "電話,傳真,地址,Email".Split(new char[] { ',' })))
            {                
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }
    }
}