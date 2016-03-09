using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using Laundry.DAL;

namespace Laundry.BLL
{
  public class LaundryManBs
    {
        private LaundryManDA NewLaundryManDA = new LaundryManDA();

        public IEnumerable<LaundryMan> ListAll()
        {
            return NewLaundryManDA.ListAll();
        }


        public IEnumerable<LaundryMan> ListAllByStatus(string status)
        {
            return NewLaundryManDA.ListAllByStatus(status);
        }

        public string UpdateStatus(string username, string status)
        {
            return NewLaundryManDA.UpdateStatus(username,status);
        }
        public LaundryMan GetById(string id)
        {
            return NewLaundryManDA.GetById(id);
        }

        public LaundryMan GetByUsername(string username)
        {
            return NewLaundryManDA.GetByUsername(username);
        }

        public string VerifyUsername(string username)
        {
            return NewLaundryManDA.VerifyUsername(username);
        }
        public void Insert(LaundryMan LaundryManBsObj)
        {
            NewLaundryManDA.Insert(LaundryManBsObj);
        }

        public void Update(LaundryMan LaundryManBsObj)
        {
            var LaundryManExist = NewLaundryManDA.GetById(LaundryManBsObj.Username);
            LaundryManExist.Password = LaundryManBsObj.Password;
            //LaundryManExist.Amount = LaundryManBsObj.Amount;
            LaundryManExist.Flag = LaundryManBsObj.Flag;
            NewLaundryManDA.Update(LaundryManExist);
        }

        public string Login(string username, string password)
        {
          return NewLaundryManDA.Login(username,password);
        }

        }
    }
