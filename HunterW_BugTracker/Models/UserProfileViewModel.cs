using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HunterW_BugTracker.Models
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [MaxLength(30, ErrorMessage = "Name must be less than 30 characters.")]
        [MinLength(1, ErrorMessage = "Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "Name must be less than 30 characters.")]
        [MinLength(1, ErrorMessage = "Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(20, ErrorMessage = "Display name must be less than 20 characters.")]
        [MinLength(4, ErrorMessage = "Display name must be at least 4 characters.")]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Avatar path")]
        public string AvatarUrl { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public HttpPostedFileBase Avatar { get; set; }
    }
}