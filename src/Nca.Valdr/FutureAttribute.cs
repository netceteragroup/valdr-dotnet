﻿namespace Nca.Valdr
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    /// <summary>
    /// Future date validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FutureAttribute : ValidationAttribute
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FutureAttribute()
        {
            ErrorMessage = "{0} must be in the future";
        }

        /// <summary>
        /// Determines whether the specified date is in the future.
        /// </summary>
        /// <param name="value">The value of the date.</param>
        /// <returns>true if the specified date is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date > DateTime.Now;
            }

            return false;
        }

        /// <summary>
        /// Formatting the error message.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}
