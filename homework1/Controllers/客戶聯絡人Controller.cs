using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using homework1.Models;
using System.IO;
using PagedList;

namespace homework1.Controllers
{
    [Authorize(Roles = "board_admin")]
    public class 客戶聯絡人Controller : BaseController
    {        
        private int pageSize = 2;

        // GET: 客戶聯絡人
        [HandleError(ExceptionType = typeof(ArgumentException), View = "CustomError")]
        public ActionResult Index(string 職稱列表, int page = 1, string keyword = "")
        {
            if (keyword.Contains("'"))
            {
                throw new ArgumentException("關鍵字含有特殊字元'");
            }

            if (keyword.Contains("@"))
            {
                throw new InvalidOperationException("關鍵字含有特殊字元@");
            }

            int currentPage = page < 1 ? 1 : page;

            TempData["keyword"] = keyword;
            TempData["職稱列表"] = 職稱列表;

            var data = repo客戶聯絡人.searchKeyword(keyword)
                .OrderBy(p => p.姓名).ToList();

            if (!string.IsNullOrEmpty(職稱列表))
            {
                data = data.Where(p => p.職稱 == 職稱列表).ToList();
            }

            var result = data.ToPagedList(currentPage, pageSize);

            ViewBag.職稱列表 = new SelectList(repo客戶聯絡人.get職稱列表(), "", "");

            return View(result);
        }


        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {                
                repo客戶聯絡人.Add(客戶聯絡人);
                repo客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {                
                var db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                repo客戶聯絡人.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo客戶資料.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo客戶聯絡人.Find(id);
            repo客戶聯絡人.Delete(客戶聯絡人);
            repo客戶聯絡人.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (客戶資料Entities)repo客戶聯絡人.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult 客戶聯絡人partialView(int id)
        {
            var 客戶聯絡人 = repo客戶聯絡人.All().Where(p => p.客戶Id == id).Include(客 => 客.客戶資料).OrderBy(p => p.姓名);
            return PartialView(客戶聯絡人);
        }

        public FileResult 匯出聯絡人資料()
        {
            //建立Excel文件
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //新增sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //取得匯出資料List
            List<客戶聯絡人> dataList = repo客戶聯絡人.All().ToList();
            //给sheet1添加第一行的標題列
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("職稱");
            row1.CreateCell(1).SetCellValue("姓名");
            row1.CreateCell(2).SetCellValue("Email");
            row1.CreateCell(3).SetCellValue("手機");
            row1.CreateCell(4).SetCellValue("地址電話");
            row1.CreateCell(5).SetCellValue("Email");
            row1.CreateCell(6).SetCellValue("客戶名稱");
            //將資料逐筆寫入
            for (int i = 0; i < dataList.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(dataList[i].職稱.ToString());
                rowtemp.CreateCell(1).SetCellValue(dataList[i].姓名.ToString());
                rowtemp.CreateCell(2).SetCellValue(dataList[i].Email.ToString());
                rowtemp.CreateCell(3).SetCellValue(dataList[i].手機.ToString());
                rowtemp.CreateCell(4).SetCellValue(dataList[i].電話.ToString());
                rowtemp.CreateCell(5).SetCellValue(dataList[i].Email.ToString());
                rowtemp.CreateCell(6).SetCellValue(dataList[i].客戶資料.客戶名稱.ToString());
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "客戶聯絡人清單.xls");
        }
    }
}
