using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Model
{
   public  class Remittance
    {
        [Key]
        public int Remittance_Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Remit Date")]
        [Required]
        public DateTime Remittance_Date { get; set; }

        [Display(Name = "Remit Amount")]
        [Required]
        public double Remittance_Amount { get; set; }

        [Display(Name = "Special Note")]
        public string Special_Note { get; set; }

        [Display(Name = "Status")]
        public string Remittance_Status { get; set; }

        public string Remittance_Flag { get; set; }

        [Display(Name = "Remit User Id")]
        public string Remittance_UserId { get; set; }

        [Display(Name = "Remit Entry Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Remittance_KeyDate { get; set; }
    }
}
