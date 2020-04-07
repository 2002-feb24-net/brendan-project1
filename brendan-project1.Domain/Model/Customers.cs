using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brendan_project1
{
    public partial class Customers
    {
        
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        /*[Key]
        [Display(Name = "Customer ID")]*/
        public int CustomerId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(40, ErrorMessage = "Maximum name length is 40")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(40, ErrorMessage = "Maximum name length is 40")]
        public string LastName { get; set; }
        [DataType(DataType.MultilineText)]
        //[MaxWordAttributes(50)]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number (10 digits)")]
        public string Phone { get; set; }
        //[Required(ErrorMessage = "City number is required")]
  
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        //public virtual DbSet<Customers> Customers { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
