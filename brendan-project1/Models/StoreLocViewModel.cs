using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace brendan_project1.Models
{
    public class StoreLocViewModel
    {
        public int StoreId { get; set; }
        [Display(Name = "Location Name")]
        [MaxLength(40, ErrorMessage = "Max name length is 40")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
