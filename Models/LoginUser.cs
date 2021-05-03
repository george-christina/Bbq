using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Bbq.Models
{
    public class LoginUser
    {
        [EmailAddress]
        [Display(Name="Email:")]
        [Required]
        public string LoginEmail {get;set;}

        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage="Password must be at least 8 characters")]
        [Display(Name="Password")]
        [Required]
        public string LoginPassword {get;set;}
    }
}