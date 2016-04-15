using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace homework1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Include(客 => 客.客戶資料).Where(p => p.IsDelete == false).OrderBy(p => p.帳戶名稱);
        }

        public IQueryable<客戶銀行資訊> All(bool IsAll)
        {
            if (IsAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.IsDelete = true;
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<客戶銀行資訊> searchKey(string key)
        {
            return base.All().Where(p => p.銀行名稱.Contains(key) || p.帳戶名稱.Contains(key) || p.帳戶號碼.Contains(key) || p.銀行代碼.ToString().Contains(key) || p.分行代碼.ToString().Contains(key) || p.客戶資料.客戶名稱.Contains(key))
                .Include(客 => 客.客戶資料);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}