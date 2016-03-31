using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace homework1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.IsDelete == false).OrderBy(p => p.姓名);
        }

        public IQueryable<客戶聯絡人> All(bool IsAll)
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