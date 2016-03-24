using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace homework1.Models
{
    public class CheckMobileFormatAttribute : DataTypeAttribute
    {
        public CheckMobileFormatAttribute() : base(DataType.PhoneNumber)
        {

        }

        public override bool IsValid(object value)
        {
            var strPhoneNumber = Convert.ToString(value);
            var pattern = @"\d{4}-\d{6}";
            Regex Reg = new Regex(pattern);
            var match = Reg.Matches(strPhoneNumber);
            var match2 = Reg.Replace(strPhoneNumber, "");            
            return match.Count == 1 && String.IsNullOrEmpty(match2);
        }
    }
}