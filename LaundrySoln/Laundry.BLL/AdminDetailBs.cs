using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using Laundry.DAL;

namespace Laundry.BLL
{
  public  class AdminDetailBs
    {
        private AdminDetailDA NewAdminDetailDA = new AdminDetailDA();
        public IEnumerable<AdminDetail> ListAll()
        {
            return NewAdminDetailDA.ListAll();
        }

      
        public AdminDetail GetById(int id)
        {
            return NewAdminDetailDA.GetById(id);
        }

        //public string VerifyUsername(string username)
        //{
        //    return NewAdminDetailDA.VerifyUsername(username);
        //}
        public void Insert(AdminDetail AdminDetailBsObj)
        {
            NewAdminDetailDA.Insert(AdminDetailBsObj);
        }

        public void Update(AdminDetail AdminDetailBsObj)
        {
            var AdminDetailExist = NewAdminDetailDA.GetById(AdminDetailBsObj.AdminDetail_Id);
            AdminDetailExist.Admin_Password = AdminDetailBsObj.Admin_Password;
            AdminDetailExist.Admin_Flag = AdminDetailBsObj.Admin_Flag;
            NewAdminDetailDA.Update(AdminDetailExist);
        }

        public string Login(string username, string password)
        {
            return NewAdminDetailDA.Login(username, password);
        }

    }
}
