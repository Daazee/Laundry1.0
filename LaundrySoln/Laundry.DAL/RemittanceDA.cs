using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;

namespace Laundry.DAL
{
   public class RemittanceDA
    {
        private LaundryContext context = new LaundryContext();
        string message;

        public IEnumerable<Remittance> GetAllRemittance()
        {
            return context.Remittances.ToList();
        }

        public Remittance GetById(int id)
        {
            return context.Remittances.Where(c => c.Remittance_Id == id).FirstOrDefault();
        }

        public Remittance GetByRemitDate_UserId(DateTime remitdate, string userId)

        {

            //return context.Remittances.Where(c => c.Remittance_Date == remitdate).Where(c=>c.Remittance_UserId==userId).FirstOrDefault();
            return context.Remittances.Where(c => c.Remittance_Date == remitdate).Where(c => c.Remittance_UserId == userId).FirstOrDefault();
        }

        public IEnumerable<Remittance> GetMyRemittance(DateTime StartDate, DateTime EndDate, string userId)

        {

            return context.Remittances.Where(c => c.Remittance_KeyDate >= StartDate).Where(c => c.Remittance_KeyDate <= EndDate)
                                            .Where(c=>c.Remittance_UserId==userId).ToList();
        }

        public IEnumerable<Remittance> GetRemittanceByDate(DateTime StartDate, DateTime EndDate)
        {

            return context.Remittances.Where(c => c.Remittance_KeyDate >= StartDate).Where(c => c.Remittance_KeyDate <= EndDate)
                                            .ToList();
        }
        public void Insert(Remittance RemittancesDAObj)
        {
            context.Remittances.Add(RemittancesDAObj);
            context.SaveChanges();
        }

        public void Update(Remittance RemittancesDAObj)
        {
            context.Entry(RemittancesDAObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var search = context.Remittances.Where(c => c.Remittance_Id == id).FirstOrDefault();
            context.Remittances.Remove(search);
            context.SaveChanges();
        }


        public string CorfirmRemit(int RemittanceId, string status)
        {
            // var search = context.Transactions.Where(c => c.TransactionId == Transid).FirstOrDefault();
            var search = GetById(RemittanceId);
            search.Remittance_Status = status;
            context.SaveChanges();
            message = "Remittance confirmed successfully";
            return message;
        }
    }
}
