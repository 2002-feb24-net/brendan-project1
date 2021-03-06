﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brendan_project1
{
    public partial class Foods
    {
        public Foods()
        {
            Inventory = new HashSet<Inventory>();
            Orderline = new HashSet<Orderline>();
        }

        public int FoodId { get; set; }
        [Display(Name = "FoodType Name")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orderline> Orderline { get; set; }
    }
}
