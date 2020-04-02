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

        [Key]
        [Display(Name = "Customer ID")]
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
        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter PhoneNumber as 0123456789, 012-345-6789, (012)-345-6789.")]
        public string Phone { get; set; }
  
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public int Id { get; internal set; }
    }
}
