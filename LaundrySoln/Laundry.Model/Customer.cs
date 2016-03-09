using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Model
{
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }
        public string Surname { get; set; }
        public string Othername { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public string Flag { get; set; }
        public DateTime Keydate { get; set; }
    }
}
