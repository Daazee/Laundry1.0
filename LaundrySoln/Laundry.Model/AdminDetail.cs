using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Laundry.Model
{
  public  class AdminDetail
    {
        [Key]
        public int AdminDetail_Id { get; set; }

        [Display(Name ="Surname")]
        public string Admin_Surname { get; set; }
        [Display(Name = "Othername")]
        public string Admin_Othername { get; set; }

        [Display(Name = "Sex")]
        public string Admin_Sex { get; set; }

        [Display(Name = "Phone Number")]
        public string Admin_PhoneNumber { get; set; }

        [Display(Name = "Address")]
        public string Admin_Address { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Admin_Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Reg_Status { get; set; }
        public string Admin_Password { get; set; }
        public string Admin_Flag { get; set; }
        [Display(Name = "Key Date")]
        public DateTime Admin_Keydate { get; set; }
    }
}
