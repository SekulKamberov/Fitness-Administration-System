namespace FAS.Common.Validation.Attributes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.ComponentModel.DataAnnotations;

    public class ValidPhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var phone = value as string;

            if (phone is null)
            {
                return false;
            }

            string pattern = @"^\+\d{10,12}$";

            Regex rgx = new Regex(pattern);

            return rgx.IsMatch(phone);
        }
    }
}
