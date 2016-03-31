using System;
using System.Linq;
using System.Collections.Generic;
	
namespace homework1.Models
{   
	public  class 客戶分類Repository : EFRepository<客戶分類>, I客戶分類Repository
	{
        public override IQueryable<客戶分類> All()
        {
            return base.All().Where(p => p.IsDelete == false).OrderBy(p => p.客戶分類名稱);
        }

        public IQueryable<客戶分類> All(bool IsAll)
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

        public override void Delete(客戶分類 entity)
        {
            entity.IsDelete = true;
        }

        public 客戶分類 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶分類Repository : IRepository<客戶分類>
	{

	}
}