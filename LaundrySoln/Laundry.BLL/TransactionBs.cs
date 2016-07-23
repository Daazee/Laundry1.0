using System.Collections.Generic;
using System.Linq;
using Laundry.Model;
using Laundry.DAL;
using System;

namespace Laundry.BLL
{
    public class TransactionBs
    {
        private TransactionDA NewTransactionDA = new TransactionDA();
        private string message;
        public IEnumerable<Transaction> ListAll()
        {
            return NewTransactionDA.GetAll();
        }

        public Transaction GetById(int id)
        {
            return NewTransactionDA.GetById(id);
        }

        public void Insert(Transaction TransactionBsObj)
        {
            NewTransactionDA.Insert(TransactionBsObj);
        }

        public void Update(Transaction TransactionBsObj)
        {
            var TransactionExist = NewTransactionDA.GetById(TransactionBsObj.TransactionId);
            TransactionExist.Flag = TransactionBsObj.Flag;
            NewTransactionDA.Update(TransactionExist);
        }

        public void TransactionUpdate(Transaction TransactionBsObj)
        {
            var TransactionExist = NewTransactionDA.GetByTransactionNo(TransactionBsObj.TransactionNo);
            foreach (Transaction item in TransactionExist)
            {
                item.AmountPaid = item.AmountPaid + TransactionBsObj.Balance;
                item.Balance = item.TotalCostAmount - item.AmountPaid;
                NewTransactionDA.Update(item);
            }
        }

        public int GetLastTransNo(string TNo)
        {
            return NewTransactionDA.GetLastTransNo(TNo);
        }

        public IEnumerable<Transaction> GetByTransactionNo(string TNo)
        {
            return NewTransactionDA.GetByTransactionNo(TNo);
        }

        public IEnumerable<Transaction> Search(string type, string value, DateTime startdate, DateTime enddate)
        {
            return NewTransactionDA.Search(type, value, startdate, enddate);
        }

        public string UpdateClothStatus(int Transid, string status)
        {
            var search = NewTransactionDA.GetById(Transid);
            if (status == "C")
            {
                if (search.ClothStatus == "C")
                {
                    message = "Cloth has already been collected";
                    return message;
                }
                else if (search.ClothStatus == "R") //Ready Starts here
                {
                    if (search.TotalCostAmount != search.AmountPaid)
                    {
                        message = "Customer has not balance his/her payment, please update payment.";
                        return message;
                    }
                    else
                    {
                        search.ClothStatus = status;
                        NewTransactionDA.UpdateClothStatus(Transid, status);
                        message = "Status updated to Collected successfully";
                        return message;
                    }
                }  //Ready Ends here
                //else if (search.ClothStatus == "N")
                else
                {
                    message = "Cloth is not ready for collection, please check cloth status";
                    return message;
                }
            }

            if (status == "R")
            {
                if (search.ClothStatus == "C")// Already collected
                {
                    message = "Cloth has already been collected";
                    return message;
                }
                else
                 if (search.ClothStatus == "R")// Already marked Ready
                {
                    message = "Cloth has already been marked Ready";
                    return message;
                }

            }
            NewTransactionDA.UpdateClothStatus(Transid, status);
            message = "Status updated to Ready successfully";
            return message;
        }

        public Transaction GetSingleTransaction(string TNo)
        {
            return NewTransactionDA.GetSingleTransaction(TNo);
        }
    }

}
