﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Training.Portals.Models
{
    public class RegisterModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "User's name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [Display(Name = "User's login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}