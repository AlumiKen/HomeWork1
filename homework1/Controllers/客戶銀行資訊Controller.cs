using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using homework1.Models;
using System.IO;

namespace homework1.Controllers
{
    public class 客戶銀行資訊Controller : BaseController
    {       
        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            var 客戶銀行資訊 = repo客戶銀行資訊.All().Include(客 => 客.客戶資料);
            return View(客戶銀行資訊.ToList());
        }

        //搜尋
        [HttpPost]
        public ActionResult Index(string key)
        {
            //var 客戶銀行資訊 = db.Database.SqlQuery<客戶銀行資訊>(
            //    @"SELECT * FROM dbo.客戶銀行資訊 WHERE 銀行名稱 LIKE @p0 OR 銀行代碼 LIKE @p0 OR 分行代碼 LIKE @p0 OR 帳戶名稱 LIKE @p0 OR 帳戶號碼 LIKE @p0", "%" + key + "%").OrderBy(p => p.帳戶名稱);
            var 客戶銀行資訊 = repo客戶銀行資訊.All()
                .Where(p => p.銀行名稱.Contains(key) || p.帳戶名稱.Contains(key) || p.帳戶號碼.Contains(key) || p.銀行代碼.ToString().Contains(key) || p.分行代碼.ToString().Contains(key) || p.客戶資料.客戶名稱.Contains(key))
                .Include(客 => 客.客戶資料);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo客戶資料.Where(p => p.IsDelete == false), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {                
                repo客戶銀行資訊.Add(客戶銀行資訊);
                repo客戶銀行資訊.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                var db = repo客戶銀行資訊.UnitOfWork.Context;
                db.Entry(客戶銀行資訊).State = EntityState.Modified;
                repo客戶銀行資訊.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = repo客戶銀行資訊.Find(id);
            客戶銀行資訊.IsDelete = true;
            repo客戶銀行資訊.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = repo客戶銀行資訊.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileResult 匯出客戶銀行資訊()
        {
            //建立Excel文件
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //新增sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //取得匯出資料List
            List< 客戶銀行資訊> dataList = repo客戶銀行資訊.All().ToList();
            //给sheet1添加第一行的標題列
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("銀行名稱");
            row1.CreateCell(1).SetCellValue("銀行代碼");
            row1.CreateCell(2).SetCellValue("分行代碼");
            row1.CreateCell(3).SetCellValue("帳戶名稱");
            row1.CreateCell(4).SetCellValue("帳戶號碼");            
            row1.CreateCell(5).SetCellValue("客戶名稱");
            //將資料逐筆寫入
            for (int i = 0; i < dataList.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(dataList[i].銀行名稱.ToString());
                rowtemp.CreateCell(1).SetCellValue(dataList[i].銀行代碼.ToString());
                rowtemp.CreateCell(2).SetCellValue(dataList[i].分行代碼.ToString());
                rowtemp.CreateCell(3).SetCellValue(dataList[i].帳戶名稱.ToString());
                rowtemp.CreateCell(4).SetCellValue(dataList[i].帳戶號碼.ToString());                
                rowtemp.CreateCell(5).SetCellValue(dataList[i].客戶資料.客戶名稱.ToString());
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "客戶銀行資訊清單.xls");
        }
    }
}
