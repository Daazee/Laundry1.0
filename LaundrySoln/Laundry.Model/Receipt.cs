using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model
{
   public class Receipt
    {
        public ICollection<Transaction> TransactionReceipt { get; set; }

        public ICollection<CompanyDetail> CompanyDetailReceipt { get; set; }

        public ICollection<PaymentDetail> PaymentDetailReceipt { get; set; }

        public LaundryMan LaundryManReceipt { get; set; }
    }
}
