using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.DAL;
using Laundry.Model;

namespace Laundry.BLL
{
    public class RemittanceBs
    {
        private RemittanceDA NewRemittanceDA = new RemittanceDA();
        HashHelper NewHashHelper = new HashHelper();
        string message;
        public IEnumerable<Remittance> ListAll()
        {
            return NewRemittanceDA.GetAllRemittance();
        }

        public Remittance GetById(int id)
        {
            return NewRemittanceDA.GetById(id);
        }

        public Remittance GetByRemitDate_UserId(string date, string userId)
        {
            DateTime remitdate;
            //if (!DateTime.TryParse(NewHashHelper.ValidateDate(date), out remitdate))
            //{
            //    message= "Remit Date is not valid";
            //    return null;
            //}
            //else
            //{
            //    remitdate = Convert.ToDateTime(NewHashHelper.ValidateDate(date));
            //}
           
            remitdate = Convert.ToDateTime(date);
            return NewRemittanceDA.GetByRemitDate_UserId(remitdate, userId);
        }

        public IEnumerable<Remittance> GetMyRemittance(DateTime StartDate, DateTime EndDate, string userId)
        {
            
            return NewRemittanceDA.GetMyRemittance(StartDate, EndDate, userId);
        }

        public IEnumerable<Remittance> GetRemittanceByDate(DateTime StartDate, DateTime EndDate)
        {
            return NewRemittanceDA.GetRemittanceByDate(StartDate, EndDate);
        }
        public void Insert(Remittance RemittanceObj)
        {
            NewRemittanceDA.Insert(RemittanceObj);
        }

        public void Update(Remittance RemiitanceObj)
        {

            var RemittanceExist = NewRemittanceDA.GetByRemitDate_UserId(RemiitanceObj.Remittance_Date,RemiitanceObj.Remittance_UserId);
            if (RemittanceExist != null)
            {
                RemittanceExist.Remittance_Amount = RemiitanceObj.Remittance_Amount;
                RemittanceExist.Special_Note=RemiitanceObj.Special_Note;
                RemittanceExist.Remittance_Flag = RemiitanceObj.Remittance_Flag;
                NewRemittanceDA.Update(RemittanceExist);
            }
            //track if user change d date initially entered
            //if changed RemiitanceExist will result to null

        }
        public string CorfirmRemit(int Transid, string status)
        {
            return NewRemittanceDA.CorfirmRemit(Transid, status);
        }
    }
}
