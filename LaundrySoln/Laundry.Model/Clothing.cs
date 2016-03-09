using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Laundry.Model
{
  public  class Clothing
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sn { get; set; }

        [DisplayName("Cloth Code")]
        public string ClothId { get; set; }

        [DisplayName("Cloth Description")]
        public string ClothDesc { get; set; }

        public double Amount { get; set; }
        public string UserId { get; set; }
        public string Flag { get; set; }
        public DateTime Keydate { get; set; }
    }
}
