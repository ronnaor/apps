using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Manager
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "Must be only chars")]
        [Display(Name = "First Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "Must be only chars")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Password does not match given password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}