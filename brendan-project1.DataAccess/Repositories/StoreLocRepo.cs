using brendan_project1.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace brendan_project1.DataAccess.Repositories
{
    class StoreLocRepo : IStoreLocRepo
    {
        private readonly RestaurantAfrikContext context = new RestaurantAfrikContext();
        public IEnumerable<StoreLoc> GetStoreLocs()
        {
            return context.StoreLoc.Include("F.F");
        }

        /// <summary>
        /// Adds a customer to database
        /// </summary>
        /// <param name="cust"></param>
        public void Add(StoreLoc l)
        {
            context.StoreLoc.Add(l);
        }

        /// <summary>
        /// Sets location's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(StoreLoc l)
        {
            context.Entry(l).State = EntityState.Modified;
        }
        public void UpdateInventory(int id, int qty)
        {
            var to_update = context.Inventory.Find(id);
            to_update.Quantity -= qty;
            context.SaveChanges();
        }
        public int GetQty(int id)
        {
            return context.Inventory.Find(id).Quantity;
        }
        public List<Inventory> GetInventory(int id)
        {
            var listInventoryModel = new List<Inventory>();
            listInventoryModel = context.Inventory
                                            .Include("F")
                                            .Where(i => i.StoreId == id)
                                            .ToList();

            return listInventoryModel;
        }

        public List<StoreLoc> GetList()
        {
            return context.StoreLoc.ToList();
        }
    }
}
