using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace brendan_project1.Models
{
    public class OrderViewModel
    {
        public List<Inventory> Inventory { get; set; }
        public Foods Item { get; set; }
    }
}
