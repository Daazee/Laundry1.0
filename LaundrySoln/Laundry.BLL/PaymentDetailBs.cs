using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using Laundry.DAL;

namespace Laundry.BLL
{
    public class PaymentDetailBs
    {
        private LaundryContext context = new LaundryContext();

        private PaymentDetailDA NewPaymentDetailDA = new PaymentDetailDA();
        public IEnumerable<PaymentDetail> GetAll()
        {
            return NewPaymentDetailDA.GetAll();
        }

        public PaymentDetail GetById(int id)
        {
            return NewPaymentDetailDA.GetById(id);
        }
        public void Insert(PaymentDetail PaymentDetailDAObj)
        {
            NewPaymentDetailDA.Insert(PaymentDetailDAObj);
        }

        public void Update(PaymentDetail PaymentDetailDAObj)
        {
            NewPaymentDetailDA.Update(PaymentDetailDAObj);
        }

        public void Delete(int id)
        {
            NewPaymentDetailDA.Delete(id);
        }


        public IEnumerable<PaymentDetail> GetByPaymentNo(string PaymentNo)
        {
            return NewPaymentDetailDA.GetByPaymentNo(PaymentNo);
        }

        //public IEnumerable<PaymentDetail> Search(string type, string value, DateTime startdate, DateTime enddate)
        //{
        //    NewPaymentDetailDA.Search(PaymentNo); ;
        //}

        public IEnumerable<PaymentDetail> GetByPaymentDate_UserId(string date, string userId)
        {
            DateTime remitdate;
            remitdate = Convert.ToDateTime(date);
            return NewPaymentDetailDA.GetByPaymentDate_UserId(remitdate, userId);
            // var result=NewPaymentDetailDA.GetByPaymentDate_UserId(remitdate, userId);
            //foreach(var amount in result)
            //{
            //    amount.
            //}
        }
    }
}
