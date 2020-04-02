using System;
using System.Collections.Generic;

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
        public int StoreId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual StoreLoc Store { get; set; }
        public virtual ICollection<Orderline> Orderline { get; set; }
    }
}
