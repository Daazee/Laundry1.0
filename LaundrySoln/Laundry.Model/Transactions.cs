using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laundry.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [Display(Name = "Transaction Number")]
        public string TransactionNo { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Tag")]
        public string CustomerTag { get; set; }

        [Display(Name = "Phone No")]
        public string CustomerPhone { get; set; }

        [Display(Name ="Cloth Type")]
        public string ClothCode { get; set; }

        public double UnitPrice { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        [Display(Name = "Laundry Type")]
        public string LaundryType {get; set;}

        public string Colour { get; set; }
    
        public string Address { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }

        [Display(Name = "Amount Paid")]
        public double AmountPaid { get; set; }

        public double Balance { get; set; }


        [Display(Name = "Total Cost")]
        public double TotalCostAmount { get; set; }

        public double ExPressAmount { get; set; }

        public string ClothStatus { get; set; }
        public string UserId { get; set; }

        public string Flag { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime KeyDate { get; set; }

        public string HeaderDetail { get; set; }//H or D. The first entry is H while subsequent entry is D.
    }
}
