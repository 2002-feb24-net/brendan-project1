using System;
using System.Collections.Generic;

namespace brendan_project1
{
    public partial class Orderline
    {
        public int Orderline1 { get; set; }
        public int Foodid { get; set; }
        public int Orderid { get; set; }
        public int Qty { get; set; }

        public virtual Foods Food { get; set; }
        public virtual Orders Order { get; set; }
    }
}
