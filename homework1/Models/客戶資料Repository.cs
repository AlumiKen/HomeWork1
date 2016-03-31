using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace homework1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{        
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.IsDelete == false).OrderBy(p => p.客戶名稱);
        }

        public IQueryable<客戶資料> All(bool IsAll)
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

        public override void Delete(客戶資料 entity)
        {
            this.UnitOfWork.Context.Database.ExecuteSqlCommand(@"UPDATE dbo.客戶聯絡人 SET IsDelete = 1 WHERE 客戶Id = @p0", entity.Id);
            this.UnitOfWork.Context.Database.ExecuteSqlCommand(@"UPDATE dbo.客戶銀行資訊 SET IsDelete = 1 WHERE 客戶Id = @p0", entity.Id);
            entity.IsDelete = true;
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        //public DbRawSqlQuery<客戶資料> Query(string key)
        //{
        //    return this.UnitOfWork.Context.Database.SqlQuery<客戶資料>(@"SELECT * FROM dbo.客戶資料 WHERE 客戶名稱 LIKE @p0", "%" + key + "%");
        //}
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}