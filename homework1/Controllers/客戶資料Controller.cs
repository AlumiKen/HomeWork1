using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using homework1.Models;

namespace homework1.Controllers
{    
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料
        public ActionResult Index()
        {
            return View(db.客戶資料.Where(p => p.IsDelete == false).OrderBy(p => p.客戶名稱).ToList());
        }

        //搜尋
        [HttpPost]
        public ActionResult Index(string key)
        {
            //var 客戶資料 = db.Database.SqlQuery<客戶資料>(
            //    @"SELECT * FROM dbo.客戶資料 WHERE 客戶名稱 LIKE @p0 OR 統一編號 LIKE @p0 OR 地址 LIKE @p0", "%" + key + "%").OrderBy(p => p.客戶名稱);
            var 客戶資料 = db.客戶資料.Where(p => p.IsDelete == false)
                .Where(p => p.客戶名稱.Contains(key) || p.統一編號.Contains(key) || p.地址.Contains(key))
                .OrderBy(p => p.客戶名稱).ToList();
            return View(客戶資料);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.IsDelete = false;
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Database.ExecuteSqlCommand(@"UPDATE dbo.客戶聯絡人 SET IsDelete = 1 WHERE 客戶Id = @p0", id);
            db.Database.ExecuteSqlCommand(@"UPDATE dbo.客戶銀行資訊 SET IsDelete = 1 WHERE 客戶Id = @p0", id);

            客戶資料 客戶資料 = db.客戶資料.Find(id);            
            客戶資料.IsDelete = true;

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

        public ActionResult 搜尋客戶資料(string key)
        {
            var 客戶資料 = db.Database.SqlQuery<客戶資料>(@"SELECT * FROM dbo.客戶資料 WHERE 客戶名稱 LIKE @p0", "%" + key + "%");
            return View(客戶資料);
        }

        public ActionResult 客戶清單()
        {
            var 客戶清單 = db.CustomerListInfoViews;
            return View(客戶清單);
        }
    }
}
