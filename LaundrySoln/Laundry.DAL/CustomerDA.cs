using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;

namespace Laundry.DAL
{
   public class CustomerDA
    {
        private LaundryContext context = new LaundryContext();

        public IEnumerable<Customer> GetAllCustomer()
        {
            return context.Customers.ToList();
        }

        public Customer GetById(string id)
        {
            return context.Customers.Where(c=>c.CustomerId== id).FirstOrDefault();
        }
        public void Insert(Customer CustomerDAObj)
        {
            context.Customers.Add(CustomerDAObj);
            context.SaveChanges();
        }

        public void Update(Customer CustomerDAObj)
        {
            context.Entry(CustomerDAObj).State= EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var search = context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            context.Customers.Remove(search);
            context.SaveChanges();
        }
    }
}
