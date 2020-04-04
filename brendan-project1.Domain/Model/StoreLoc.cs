using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brendan_project1
{
    public partial class StoreLoc
    {
        public StoreLoc()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
        }

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

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
