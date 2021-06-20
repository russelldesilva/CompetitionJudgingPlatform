using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class Judge
    {
        public int JudgeID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Judge Name")]
        public string JudgeName{ get; set; }
        [StringLength(5)]
        public string Salutation { get; set; }
        public int AreaIntrestID { get; set; }
        public int CompetitionID { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
        [StringLength(50)]
        public string EmailAddr { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        [DefaultValue("p@55Judge")]
        public string Password { get; set; }
        public bool Selected { get; set; } = false;
    }
}
