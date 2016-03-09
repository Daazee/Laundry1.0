using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Model
{
    public class Codes
    {
        [Key]
        public int Codes_Id { get; set; }

        [Display(Name ="Code Type")]
        public string Codes_Type { get; set; }

        [Display(Name = "Code Description")]
        public string Codes_Desc { get; set; }

        [Display(Name = "Code Value")]
        public string Codes_Val { get; set; }

        public string Codes_Flag { get; set; }

        public string Codes_UserId { get; set; }

        public DateTime Codes_KeyDate { get; set; }
    }
}
