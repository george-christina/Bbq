using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bbq.Models
{
    public class User
    {
        [Key]
        [Required]
        public int UserId { get;set; }

        [Required(ErrorMessage="First Name is required")]
        [MinLength(2,ErrorMessage="First name needs at least 2 characters")]
        [Display(Name="First Name:")]
        public string FirstName { get;set; }
        
        [Required(ErrorMessage="Last name is required")]
        [MinLength(2,ErrorMessage="Last name needs at least 2 characters")]
        [Display(Name="Last Name:")]
        public string LastName { get;set; }

        [EmailAddress]
        [Required(ErrorMessage="Email is required")]
        [Display(Name="Email:")]
        public String Email { get;set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage="Password is required")]
        [MinLength(8,ErrorMessage="Password must be at least 8 characters")]
        [Display(Name="Password")]
        public string Password { get;set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name="Confirm Password")]
        [NotMapped]
        public string ConfirmPassword { get;set; }

        //One to many - A user can plan many bbqs
        public List<BbqEvent> BbqEvents { get;set; }

        public List<Rsvp> Attending { get;set; }
        public DateTime CreatedAt { get;set; } = DateTime.Now;
        public DateTime UpdatedAt { get;set; } = DateTime.Now;
    }
}