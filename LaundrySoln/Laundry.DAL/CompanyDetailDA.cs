using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;

namespace Laundry.DAL
{
   public class CompanyDetailDA
    {
        public LaundryContext context = new LaundryContext();
        private string message;
        public IEnumerable<CompanyDetail> ListAll()
        {
            return context.CompanyDetails.ToList();
        }

        public CompanyDetail GetById(int id)
        {
            return context.CompanyDetails.Where(c => c.Company_Id == id).FirstOrDefault();
        }

        public IEnumerable<CompanyDetail> GetByIdList(int id)
        {
            return context.CompanyDetails.Where(c => c.Company_Id == id).ToList();
        }

        public CompanyDetail GetByCompCode(string code)
        {
            return context.CompanyDetails.Where(c => c.Company_Code == code).FirstOrDefault();
        }
        
        public void Insert(CompanyDetail CompanyDetailObj)
        {
            context.CompanyDetails.Add(CompanyDetailObj);
            context.SaveChanges();
        }

        public void Update(CompanyDetail CompanyDetailObj)
        {
            context.Entry(CompanyDetailObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var search = context.CompanyDetails.Where(c => c.Company_Id == id).FirstOrDefault();
            context.CompanyDetails.Remove(search);
            context.SaveChanges();
        }

        public string VerifyCompanyEmail(string email)
        {
            var search = context.CompanyDetails.Where(c => c.Company_Email == email).FirstOrDefault();
            if (search != null)
            {
                message = "Company email already exist";
            }
            else
            {
                message = "";
            }
            return message;
        }
    }
}
