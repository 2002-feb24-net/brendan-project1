using System;
using System.Collections.Generic;

namespace brendan_project1
{
    public partial class Inventory
    {
        public int Quantity { get; set; }
        public int StoreId { get; set; }
        public int FoodId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Inventoryid { get; set; }

        public virtual Foods Food { get; set; }
        public virtual StoreLoc Store { get; set; }
    }
}
