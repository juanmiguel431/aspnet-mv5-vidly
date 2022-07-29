using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (ICustomer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo) return ValidationResult.Success;

            if (!customer.Birthdate.HasValue) return new ValidationResult("Birthdate is required.");

            var eighteenYears = DateTime.Now.AddYears(-18);

            return customer.Birthdate > eighteenYears 
                ? new ValidationResult("Customer should be at least 18 years old to go on a membership") 
                : ValidationResult.Success;
        }
    }
}