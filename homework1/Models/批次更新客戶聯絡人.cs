using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace homework1.Models
{
    public class 批次更新客戶聯絡人
    {
        [Required]
        public int Id { get; set; }

        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [CheckMobileFormat(ErrorMessage = "手機格式不符合, 範例:0912-345678")]
        //[RegularExpression(pattern: @"^\d{4}-\d{6}$")]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    }
}