using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace brendan_project1
{
    public partial class Orderline
    {
        public int Orderline1 { get; set; }
        public int Foodid { get; set; }
        public int Orderid { get; set; }
        [Display(Name = "Quantity")]
        public int Qty { get; set; }
        [ForeignKey("fid")]
        public int Fid { get; set; }
        public virtual Inventory P { get; set; }

        public virtual Orders O { get; set; }
        public virtual Foods Food { get; set; }
        public virtual Orders Order { get; set; }
        public bool ValidateQuantity(int qty)
        {
            return this.Qty < qty * .5 || (this.Qty < 100 && this.Qty < qty);
        }
    }
    
}
