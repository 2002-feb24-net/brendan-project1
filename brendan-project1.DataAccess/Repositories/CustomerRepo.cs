using brendan_project1.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brendan_project1.DataAccess.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        readonly RestaurantAfrikContext context = new RestaurantAfrikContext();
        private string phoneNumber;
        internal int id;
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
        public int AddCust(string firstName, string lastName, int phone)
        {
            var new_cust = new Customers
            {
                FirstName = firstName,
                Phone = phoneNumber,
               LastName = lastName,
                
            };
            try
            {
                context.Customers.Add(new_cust);
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                Console.WriteLine("First/username already exists");
            }
            return new_cust.Id;
        }
        public List<Customers> GetList()
        {
            var listCustomerModel = context.Customers.ToList();
            return listCustomerModel;
        }

        Domain.Interfaces.Customers ICustomerRepo.FindByID(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Domain.Interfaces.Customers>> ICustomerRepo.GetCusts()
        {
            throw new NotImplementedException();
        }

        public void Add(Domain.Interfaces.Customers cust)
        {
            throw new NotImplementedException();
        }

        public void Edit(Domain.Interfaces.Customers cust)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Interfaces.Customers> SearchCust(int mode = 0, params string[] search_param)
        {
            throw new NotImplementedException();
        }

        List<Domain.Interfaces.Customers> ICustomerRepo.GetList()
        {
            throw new NotImplementedException();
        }
    }
}