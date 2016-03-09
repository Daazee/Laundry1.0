using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;
namespace Laundry.DAL

{
   public class CodesDA
    {
        public LaundryContext context = new LaundryContext();
        public IEnumerable<Codes> ListAll()
        {
            return context.Code.ToList();
        }

        public Codes GetById(int id)
        {
            return context.Code.Where(c => c.Codes_Id == id).FirstOrDefault();
        }

        public IEnumerable<Codes> GetByCodeType(string code_type)
        {
            return context.Code.Where(c => c.Codes_Type == code_type).ToList();
        }
        public void Insert(Codes CodeObj)
        {
            context.Code.Add(CodeObj);
            context.SaveChanges();
        }

        public void Update(Codes CodeObj)
        {
            context.Entry(CodeObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var search = context.Code.Where(c => c.Codes_Id == id).FirstOrDefault();
            context.Code.Remove(search);
            context.SaveChanges();
        }
    }
}
