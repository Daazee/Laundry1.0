using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Model
{
   public class CompanyDetail
    {
        [Key]
        public int Company_Id { get; set; }

        public string Company_Code { get; set; }

        [Display(Name = "Company Name")]
        public string Company_Name { get; set; }

        [Display(Name = "Company Short Name")]
        public string Company_ShortName { get; set; }

        [Display(Name = "Address")]
        public string Company_Address { get; set; }

        [Display(Name = "Phone Number 1")]
        public string Company_Phone1 { get; set; }

        [Display(Name = "Phone Number 2")]
        public string Company_Phone2 { get; set; }

        [Display(Name = "Email")]
        public string Company_Email { get; set; }

        [Display(Name = "Username")]
        public string Company_Username { get; set; }

        [Display(Name = "Password")]
        public string Company_Password { get; set; }

        //The person that key this information 
        public string Company_UserId { get; set; }

        public string Company_Flag { get; set; }

        public DateTime Company_KeyDate { get; set; }
    }
}
