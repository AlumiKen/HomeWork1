using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace homework1.Models
{
    public class CheckMobileFormatAttribute : RegularExpressionAttribute
    {
        public CheckMobileFormatAttribute() : base(@"^\d{4}-\d{6}$")
        {

        }

        //public override bool IsValid(object value)
        //{
        //    var strPhoneNumber = Convert.ToString(value);
        //    var pattern = @"^\d{4}-\d{6}$";
        //    return new Regex(pattern).IsMatch(strPhoneNumber);
        //}
    }
}