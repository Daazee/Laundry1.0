using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Model
{
    public class ExpressCharge
    {
        [Key]
        public string ExpressCharge_Id { get; set; }

        public string ExPressNo { get; set; }
        public double ExpressAmount { get; set; }
        public string ExpressUserId { get; set; }
        public string ExpressFlag { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpressKeyDate { get; set; }


    }
}
