using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace brendan_project1.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(30, ErrorMessage = "Maximum username lenght is 30")]
        public string userName { get; set; }
        [Display(Name = "Password")]
        [MinLength(3, ErrorMessage = "Minium password length is 3")]
        [MaxLength(80, ErrorMessage = "Maximum password length is 80")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string passWord { get; set; }
        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
        public int Id { get; set; }
        
    }
    public class RegisterViewModel
    {
        public string userName { get; set; }
       
        public string passWord { get; set; }
      
        public bool RememberMe { get; set; }

    }
}
