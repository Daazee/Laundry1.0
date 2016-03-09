using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laundry.Model
{
    public class LaundryMan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sn { get; set; }
        public int LaundryManId { get; set; }
        public string Surname { get; set; }
        public string Othername { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Reg_Status { get; set; }
        public string UserId { get; set; }
        public string Flag { get; set; }
        public DateTime Keydate { get; set; }
    }
}
