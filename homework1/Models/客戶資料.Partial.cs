namespace homework1.Models
{
    using Controllers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            客戶資料Entities db = new 客戶資料Entities();
            客戶資料 data = db.客戶資料.Where(p => p.Account == this.Account).Where(p => p.Id != this.Id).FirstOrDefault();
            if (data != null)
            {
                yield return new ValidationResult("帳號已有人使用", new string[] { "Account" });
            }

            //yield return ValidationResult.Success;
        }
    }

    public partial class 客戶資料MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        
        [StringLength(8, ErrorMessage="欄位長度不得大於 8 個字元")]
        [Required]
        public string 統一編號 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 電話 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 傳真 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        public string Email { get; set; }

        public Nullable<bool> IsDelete { get; set; }

        public string 客戶分類Id { get; set; }

        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Display(Name ="帳號")]
        public string Account { get; set; }

        [Display(Name ="密碼")]
        public string Password { get; set; }


        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}
