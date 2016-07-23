using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using System.Data.Entity;

namespace Laundry.DAL
{
   public class TransactionDA
    {
        private string message;
        private LaundryContext context = new LaundryContext();

        public IEnumerable<Transaction> GetAll()
        {
            return context.Transactions.ToList();
        }

        public Transaction GetById(int id)
        {
            return context.Transactions.Where(c => c.TransactionId == id).FirstOrDefault();
        }
        public void Insert(Transaction TransactionDAObj)
        {
            context.Transactions.Add(TransactionDAObj);
            context.SaveChanges();
        }

        public void Update(Transaction TransactionDAObj)
        {
            context.Entry(TransactionDAObj).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var search = context.Transactions.Where(c => c.TransactionId == id).FirstOrDefault();
            context.Transactions.Remove(search);
            context.SaveChanges();
        }

        public int GetLastTransNo(string TNo)
        {
            int LastTransNo = 0;
            var search = from p in context.Transactions
                         where p.TransactionNo.StartsWith(TNo)
                         orderby p.TransactionId descending
                         //orderby p.TransactionNo descending //String Ordering does not yield accurate result
                         select p;
            var result=search.FirstOrDefault();

            if (result!=null)
            {
                string[] SplitResult = result.TransactionNo.Split('/');
                LastTransNo = Convert.ToInt32(SplitResult[2]);
            }
            return LastTransNo;
        }

        public IEnumerable<Transaction> GetByTransactionNo(string TransNo)
        {
            return context.Transactions.Where(c=>c.TransactionNo==TransNo).ToList();
        }

        public IEnumerable<Transaction> Search(string type, string value, DateTime startdate, DateTime enddate)
        {
            IEnumerable<Transaction> result =null;
            if (type == "TransNo")
            {
                result= context.Transactions.Where(c => c.TransactionNo.StartsWith(value)).ToList();
            }
            else if (type == "Name")
            {
                result = context.Transactions.Where(c => c.CustomerName==value).ToList();
            }
            else if (type == "Date")
            {
                result = context.Transactions.Where(c => c.KeyDate >= startdate).Where(c=>c.KeyDate <=enddate).ToList();
            }
            else if (type == "Type")
            {
                result = context.Transactions.Where(c => c.ClothCode==value).ToList();
            }
            return result;
        }

        public void UpdateClothStatus(int Transid, string status)
        {
            var search = context.Transactions.Where(c => c.TransactionId == Transid).FirstOrDefault();
            //if (status == "C")
            //{
            //    if (search.ClothStatus == "R")
            //    {
            //        if (search.TotalCostAmount != search.AmountPaid)
            //        {
            //            message = "Customer has not balance is payment, please update payment.";
            //            return message;
            //        }
            //        else
            //        {
            //            search.ClothStatus = status;
            //            context.SaveChanges();
            //            message = "Status updated to Collected successfully";
            //            return message;
            //        }
            //    }
            //    else if (search.ClothStatus =="N")
            //    {
            //        message = "Cloth is not ready for collection, please check cloth status";
            //        return message;
            //    }
            //    else
            //    {
            //        message = "Cloth has already been collected";
            //        return message;
            //    }
            //}

            search.ClothStatus = status;
            context.SaveChanges();
            //message = "Status updated to Ready successfully";
           // return message;
        }

        public Transaction GetSingleTransaction(string TransNo)
        {
            return context.Transactions.Where(c => c.TransactionNo == TransNo).FirstOrDefault();
        }
    }
}
