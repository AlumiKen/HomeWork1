using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Collections;

namespace homework1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Include(path => path.客戶資料).Where(p => p.IsDelete == false).OrderBy(p => p.姓名);
        }

        public IQueryable<客戶聯絡人> All(bool IsAll, string keyword, string 職稱)
        {
            if (IsAll)
            {
                return base.All().Where(p => (p.職稱.Contains(keyword) || p.姓名.Contains(keyword) || p.客戶資料.客戶名稱.Contains(keyword)) && ("" == 職稱 || p.職稱 == 職稱))
                .Include(客 => 客.客戶資料);
            }
            else
            {
                return this.All().Where(p => (p.職稱.Contains(keyword) || p.姓名.Contains(keyword) || p.客戶資料.客戶名稱.Contains(keyword)) && ("" == 職稱 || p.職稱 == 職稱))
                .Include(客 => 客.客戶資料);
            }
        }

        public IQueryable<string> get職稱列表()
        {
            return this.All().Select(p => p.職稱).Distinct();
        }

        public IQueryable<客戶聯絡人> searchKeyword(string keyword)
        {
            return this.All().Where(p => (p.職稱.Contains(keyword) || p.姓名.Contains(keyword) || p.客戶資料.客戶名稱.Contains(keyword)));
        }

        public override void Delete(客戶聯絡人 entity)
        {            
            entity.IsDelete = true;
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }        
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}