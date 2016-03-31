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
    public class 客戶資料Controller : BaseController
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料
        public ActionResult Index()
        {
            var data = repo客戶資料.All().ToList();
            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱");
            return View(data);
        }

        //搜尋
        [HttpPost]
        public ActionResult Index(string key, int? 客戶分類Id)
        {
            //var 客戶資料 = db.Database.SqlQuery<客戶資料>(
            //    @"SELECT * FROM dbo.客戶資料 WHERE 客戶名稱 LIKE @p0 OR 統一編號 LIKE @p0 OR 地址 LIKE @p0", "%" + key + "%").OrderBy(p => p.客戶名稱);
            var 客戶資料 = repo客戶資料.All()
                .Where(p => p.客戶名稱.Contains(key) || p.統一編號.Contains(key) || p.地址.Contains(key))
                .OrderBy(p => p.客戶名稱).ToList();

            if (客戶分類Id.HasValue)
            {             
                客戶資料 = 客戶資料.Where(p => p.客戶分類Id == 客戶分類Id.Value).ToList();
            }

            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱", 客戶分類Id);
            return View(客戶資料);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶聯絡人清單 = repo客戶聯絡人.All().Where(p => p.客戶Id == id.Value).ToArray();
                        
            return View(客戶資料);
        }

        [HttpPost]
        public ActionResult Details(IList<批次更新客戶聯絡人> data, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var 客戶聯絡人 = repo客戶聯絡人.Find(item.Id);
                    客戶聯絡人.職稱 = item.職稱;
                    客戶聯絡人.手機 = item.手機;
                    客戶聯絡人.電話 = item.電話;
                }

                repo客戶聯絡人.UnitOfWork.Commit();
            }
            
            ViewBag.客戶聯絡人清單 = repo客戶聯絡人.All().Where(p => p.客戶Id == id.Value).ToArray();
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱");
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
                repo客戶資料.Add(客戶資料);
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類Id")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db客戶資料 = repo客戶資料.UnitOfWork.Context;
                db客戶資料.Entry(客戶資料).State = EntityState.Modified;
                repo客戶資料.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶分類Id = new SelectList(repo客戶分類.All(), "Id", "客戶分類名稱", 客戶資料.客戶分類Id);
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo客戶資料.Find(id.Value);
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
            //db.Database.ExecuteSqlCommand(@"UPDATE dbo.客戶聯絡人 SET IsDelete = 1 WHERE 客戶Id = @p0", id);
            //db.Database.ExecuteSqlCommand(@"UPDATE dbo.客戶銀行資訊 SET IsDelete = 1 WHERE 客戶Id = @p0", id);

            客戶資料 客戶資料 = repo客戶資料.Find(id);
            repo客戶資料.Delete(客戶資料);

            repo客戶資料.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db客戶資料 = (客戶資料Entities)repo客戶資料.UnitOfWork.Context;
                db客戶資料.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult 搜尋客戶資料(string key)
        //{
        //    var 客戶資料 = repo客戶資料.Query(key);
        //    return View(客戶資料);
        //}

        public ActionResult 客戶清單()
        {
            var db客戶資料 = (客戶資料Entities)repo客戶資料.UnitOfWork.Context;
            var 客戶清單 = db客戶資料.CustomerListInfoViews;
            return View(客戶清單);
        }

        public FileResult 匯出客戶資料()
        {            
            //建立Excel文件
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //新增sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //取得匯出資料List
            List<客戶資料> dataList = repo客戶資料.All().ToList();
            //给sheet1添加第一行的標題列
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("客戶名稱");
            row1.CreateCell(1).SetCellValue("統一編號");
            row1.CreateCell(2).SetCellValue("電話");
            row1.CreateCell(3).SetCellValue("傳真");
            row1.CreateCell(4).SetCellValue("地址");
            row1.CreateCell(5).SetCellValue("Email");
            row1.CreateCell(6).SetCellValue("客戶分類名稱");
            //將資料逐筆寫入
            for (int i = 0; i < dataList.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(dataList[i].客戶名稱.ToString());
                rowtemp.CreateCell(1).SetCellValue(dataList[i].統一編號.ToString());
                rowtemp.CreateCell(2).SetCellValue(dataList[i].電話.ToString());
                rowtemp.CreateCell(3).SetCellValue(dataList[i].傳真.ToString());
                rowtemp.CreateCell(4).SetCellValue(dataList[i].地址.ToString());
                rowtemp.CreateCell(5).SetCellValue(dataList[i].Email.ToString());
                rowtemp.CreateCell(6).SetCellValue(dataList[i].客戶分類.客戶分類名稱.ToString());                
            }            
            
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "客戶資料清單.xls");
        }
    }
}
