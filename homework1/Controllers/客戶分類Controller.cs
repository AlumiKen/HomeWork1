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
    public class 客戶分類Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶分類
        public ActionResult Index()
        {
            return View(db.客戶分類.ToList());
        }

        // GET: 客戶分類/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶分類 客戶分類 = db.客戶分類.Find(id);
            if (客戶分類 == null)
            {
                return HttpNotFound();
            }
            return View(客戶分類);
        }

        // GET: 客戶分類/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶分類/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶分類名稱,IsDelete")] 客戶分類 客戶分類)
        {
            if (ModelState.IsValid)
            {
                db.客戶分類.Add(客戶分類);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶分類);
        }

        // GET: 客戶分類/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶分類 客戶分類 = db.客戶分類.Find(id);
            if (客戶分類 == null)
            {
                return HttpNotFound();
            }
            return View(客戶分類);
        }

        // POST: 客戶分類/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶分類名稱,IsDelete")] 客戶分類 客戶分類)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶分類).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶分類);
        }

        // GET: 客戶分類/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶分類 客戶分類 = db.客戶分類.Find(id);
            if (客戶分類 == null)
            {
                return HttpNotFound();
            }
            return View(客戶分類);
        }

        // POST: 客戶分類/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶分類 客戶分類 = db.客戶分類.Find(id);
            客戶分類.IsDelete = true;
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
