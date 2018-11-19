using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class DescriptionAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string text = value as string;

        return text != null && text.Length <= 2000;
    }
}