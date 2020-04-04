using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace brendan_project1
{
    public partial class Orders
    {
        public Orders()
        {
            Orderline = new HashSet<Orderline>();
        }

        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderTime { get; set; }
        //[Required(ErrorMessage = "Your Location name is required")]
        public int StoreId { get; set; }
        //[Required(ErrorMessage = "Customer name is required")]
        public int CustomerId { get; set; }
        //public DateTime Timestamp { get; set; }
        //public decimal? Total { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual StoreLoc Store { get; set; }
        public virtual ICollection<Orderline> Orderline { get; set; }
    }
}
