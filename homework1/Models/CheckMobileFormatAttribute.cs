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
            var strPhoneNumber = (string)value;
            var pattern = @"\d{4}-\d{6}";
            Regex Reg = new Regex(pattern);
            return Reg.IsMatch(strPhoneNumber);
        }
    }
}