using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;

namespace Laundry.DAL
{
  public  class PaymentDetailDA
    {
        private LaundryContext context = new LaundryContext();

        public IEnumerable<PaymentDetail> GetAll()
        {
            return context.PaymentDetails.ToList();
        }

        public PaymentDetail GetById(int id)
        {
            return context.PaymentDetails.Where(c => c.PaymentId == id).FirstOrDefault();
        }
        public void Insert(PaymentDetail PaymentDetailDAObj)
        {
            context.PaymentDetails.Add(PaymentDetailDAObj);
            context.SaveChanges();
        }

        public void Update(PaymentDetail PaymentDetailDAObj)
        {
            context.Entry(PaymentDetailDAObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var search = context.PaymentDetails.Where(c => c.PaymentId == id).FirstOrDefault();
            context.PaymentDetails.Remove(search);
            context.SaveChanges();
        }


        public IEnumerable<PaymentDetail> GetByPaymentNo(string PaymentNo)
        {
            return context.PaymentDetails.Where(c => c.PaymentNo == PaymentNo).ToList();
        }

        public IEnumerable<PaymentDetail> Search(string type, string value, DateTime startdate, DateTime enddate)
        {
            IEnumerable<PaymentDetail> result = null;
            if (type == "TransNo")
            {
                result = context.PaymentDetails.Where(c => c.PaymentNo.StartsWith(value)).ToList();
            }
            else if (type == "Name")
            {
                result = context.PaymentDetails.Where(c => c.CustomerName == value).ToList();
            }
            else if (type == "Date")
            {
                result = context.PaymentDetails.Where(c => c.KeyDate >= startdate).Where(c => c.KeyDate <= enddate).ToList();
            }
            return result;
        }

        public IEnumerable<PaymentDetail> GetByPaymentDate_UserId(DateTime remitdate, string userId)

        {
            return context.PaymentDetails.Where(c => c.KeyDate == remitdate).Where(c => c.UserId == userId).ToList();
            //return context.PaymentDetails.Sum(c=>c.AmountPaid,c=>cc.)
        }
    }
}
