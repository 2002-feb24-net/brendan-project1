using brendan_project1.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace brendan_project1.DataAccess.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        readonly RestaurantAfrikContext context = new RestaurantAfrikContext();
     

        public void Remove(int id)
        {
            var toRemove = context.Customers.Find(id);
            context.Customers.Remove(toRemove);
            context.SaveChanges();
        }
        public Customers FindByID(int id)
        {
            return context.Customers.Find(id);
        }
        public async Task<IEnumerable<Customers>> GetCusts()
        {
            return await context.Customers.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cust"></param>
        public void Add(Customers cust)
        {
            context.Customers.Add(cust);
            context.SaveChanges();

        }
        public void Edit(Customers cust)
        {
            context.Entry(cust).State = EntityState.Modified;
        }
        public int NumberOfCustomers()
        {
            return context.Customers.ToList().Count;

        }
        
        public List<Customers> GetList()
        {
            var listCustomerModel = context.Customers.ToList();
            return listCustomerModel;
        }

        Customers ICustomerRepo.FindByID(int id)
        {
            throw new NotImplementedException();
        }
        public void SaveChanges()
        {
            throw new NotFiniteNumberException();
        }

        public IEnumerable<Customers> SearchCust( params string[] search_param)
        {
            return context.Customers.Where(c => c.FirstName == search_param[0] && c.LastName == search_param[1]);
            
        }

        //List<Customers> ICustomerRepo.GetList()
        //{
        //    throw new NotImplementedException();
        //}
    }
}