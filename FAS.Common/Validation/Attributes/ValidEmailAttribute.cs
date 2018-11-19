namespace FAS.Common
{
    using System;
    using System.Text.RegularExpressions;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value as string;

            if (email is null)
            {
                return false;
            }

            string emailPattern = @"^([a-zA-Z0-9]+[.-_]?[a-zA-Z0-9]+)(@)[a-zA-Z0-9]+(\.[a-zA-Z0-9]{2,})+$";

            Regex rgx = new Regex(emailPattern);

            return rgx.IsMatch(email);
        }
    }
}
