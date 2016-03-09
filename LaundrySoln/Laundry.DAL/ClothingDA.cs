using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Laundry.Model;

namespace Laundry.DAL
{
   public  class ClothingDA
    {
        public LaundryContext context = new LaundryContext();
        public IEnumerable<Clothing> ListAll()
        {
            return context.Clothings.ToList();
        }

        public Clothing GetById(string id)
        {
            return context.Clothings.Where(c => c.ClothId == id).FirstOrDefault();
        }
        public void Insert(Clothing ClothingDAObj)
        {
            context.Clothings.Add(ClothingDAObj);
            context.SaveChanges();
        }

        public void Update(Clothing ClothingDAObj)
        {
            context.Entry(ClothingDAObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(string id)   
        {
            var search = context.Clothings.Where(c => c.ClothId == id).FirstOrDefault();
            context.Clothings.Remove(search );
            context.SaveChanges();
        }
    }
}
