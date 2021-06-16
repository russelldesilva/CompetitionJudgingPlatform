using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class AreaInterest
    {
        [Display(Name = "ID")]
        public int AreaInterestID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Enter name of Area of Interest")]
        public string Name { get; set; }
        public List<Judge> JudgeList { get; set; }
    }
}
