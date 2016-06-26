using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;

namespace Laundry.DAL
{
    public class ExpressChargeDA
    {
        public LaundryContext context = new LaundryContext();
        public IEnumerable<ExpressCharge> ListAll()
        {
            return context.ExpressCharges.ToList();
        }

        public ExpressCharge GetById(string id)
        {
            return context.ExpressCharges.Where(c => c.ExpressCharge_Id == id).FirstOrDefault();
        }

        public void Insert(ExpressCharge ExpressChargeObj)
        {
            context.ExpressCharges.Add(ExpressChargeObj);
            context.SaveChanges();
        }

        public void Update(ExpressCharge ExpressChargeObj)
        {
            context.Entry(ExpressChargeObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var search = context.ExpressCharges.Where(c => c.ExpressCharge_Id == id).FirstOrDefault();
            context.ExpressCharges.Remove(search);
            context.SaveChanges();
        }
    }
}
