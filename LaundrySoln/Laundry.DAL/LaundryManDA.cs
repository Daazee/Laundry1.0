using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;

namespace Laundry.DAL
{
    public class LaundryManDA
    {
        public LaundryContext context = new LaundryContext();
        private string message;
        public IEnumerable<LaundryMan> ListAll()
        {
            return context.LaundryMans.Where(c=>c.Reg_Status !="AD").ToList(); // to not include admin

        }

        public IEnumerable<LaundryMan> ListAllByStatus(string status)
        {
            return context.LaundryMans.Where(c=>c.Reg_Status==status).ToList();
        }

        public string UpdateStatus(string username, string status)
        {
            var search = context.LaundryMans.Where(c => c.Username == username).FirstOrDefault();
            search.Reg_Status = status;
            context.SaveChanges();
            message = "Status updated successfully";
            return message;
        }
        public LaundryMan GetById(string id)
        {
            return context.LaundryMans.Where(c => c.Username == id).FirstOrDefault();
        }

        public LaundryMan GetByUsername(string username)
        {
            return context.LaundryMans.Where(c => c.Username == username).FirstOrDefault();

        }
        public string VerifyUsername(string username)
        {
            var search= context.LaundryMans.Where(c => c.Username == username).FirstOrDefault();
            if (search!=null)
            {
                message = "Username already exist";
            }
            else
            {
                message = "";
            }
            return message;
        }
        public void Insert(LaundryMan LaundryManDAObj)
        {
            context.LaundryMans.Add(LaundryManDAObj);
            context.SaveChanges();
        }

        public void Update(LaundryMan LaundryManDAObj)
        {
            context.Entry(LaundryManDAObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var search = context.LaundryMans.Where(c => c.Username == id).FirstOrDefault();
            context.LaundryMans.Remove(search);
            context.SaveChanges();
        }

        public string Login(string username, string password)
        {
            var search = context.LaundryMans.Where(c => c.Username == username).FirstOrDefault();
            if(search==null)
            {
                message = "Invalid username";
            }
            else if(search.Password==password)
            {
                if(search.Reg_Status =="A")
                    message = "Success";
                if (search.Reg_Status == "AD")//Admin Status
                    message = "Success";
                else if (search.Reg_Status == "P")
                    message = "User confirmation is still pending";
                else if (search.Reg_Status == "R")
                    message = "User has been rejected";
            }
            else
            {
                message = "Invalid password";
            }
            return message;
        }
    }
}
