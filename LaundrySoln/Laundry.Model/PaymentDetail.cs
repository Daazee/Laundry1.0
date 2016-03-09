using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Laundry.Model
{
    public class PaymentDetail
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        [Display(Name = "Payment Number")]
        public string PaymentNo { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Tag")]
        public string CustomerTag { get; set; }
        [Required]
        [Display(Name = "Amount Paid")]
        public double AmountPaid { get; set; }
        public string UserId { get; set; }
        public string Flag { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime KeyDate { get; set; }
    }
}
