﻿using System.ComponentModel.DataAnnotations;

namespace lab1.Validations;

public class DateInPastAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
        => value is DateTime date && date <= DateTime.Now;
}
