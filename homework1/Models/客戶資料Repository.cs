using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Security.Cryptography;

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

        public 客戶資料 GetByAccount(string account)
        {
            return this.All().FirstOrDefault(p => p.Account == account);
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public 客戶資料 Find(string Account)
        {
            return this.All().FirstOrDefault(p => p.Email == Account);
        }

        public IQueryable<客戶資料> searchKeyword(string keyword)
        {
            return this.All().Where(p => p.客戶名稱.Contains(keyword) || p.統一編號.Contains(keyword) || p.地址.Contains(keyword));
        }

        /// <summary>
        /// 雜湊密碼
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public string HashPassword(string account, string password)
        {
            account = account.ToLower();
            Byte[] data1ToHash = (new UnicodeEncoding()).GetBytes(account + password);
            byte[] hashvalue1 = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(data1ToHash);
            return Convert.ToBase64String(hashvalue1);
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