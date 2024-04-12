using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string Email {  get; set; }

        [Required(ErrorMessage = "Role is not specified.")]
        public string Role { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }

    public class AllowedRolesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedRoles;

        public AllowedRolesAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && _allowedRoles.Contains(value.ToString()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "The role is not allowed.");
        }
    }
}
