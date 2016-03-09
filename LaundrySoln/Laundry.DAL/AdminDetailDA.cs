using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;


namespace Laundry.DAL
{
 public   class AdminDetailDA
    {
        public LaundryContext context = new LaundryContext();
        private string message;
        public IEnumerable<AdminDetail> ListAll()
        {
            return context.AdminDetails.ToList();
        }

        //public string UpdateStatus(string username, string status)
        //{
        //    var search = context.AdminDetails.Where(c => c.Username == username).FirstOrDefault();
        //    search.Reg_Status = status;
        //    context.SaveChanges();
        //    message = "Status updated successfully";
        //    return message;
        //}
        public AdminDetail GetById(int id)
        {
            return context.AdminDetails.Where(c => c.AdminDetail_Id == id).FirstOrDefault();
        }

        //public string VerifyUsername(string username)
        //{
        //    var search = context.AdminDetails.Where(c => c.Username == username).FirstOrDefault();
        //    if (search != null)
        //    {
        //        message = "Username already exist";
        //    }
        //    else
        //    {
        //        message = "";
        //    }
        //    return message;
        //}
        public void Insert(AdminDetail AdminDetailDAObj)
        {
            context.AdminDetails.Add(AdminDetailDAObj);
            context.SaveChanges();
        }

        public void Update(AdminDetail AdminDetailDAObj)
        {
            context.Entry(AdminDetailDAObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var search = context.AdminDetails.Where(c => c.AdminDetail_Id == id).FirstOrDefault();
            context.AdminDetails.Remove(search);
            context.SaveChanges();
        }

        public string Login(string username, string password)
        {
            var search = context.AdminDetails.Where(c => c.Admin_Username == username).FirstOrDefault();
            if (search == null)
            {
                message = "Invalid username";
            }
            else if (search.Admin_Password == password)
            {
                    message = "Success";
            }
            else
            {
                message = "Invalid password";
            }
            return message;
        }
    }
}
