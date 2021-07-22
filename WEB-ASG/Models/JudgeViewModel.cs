using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class JudgeViewModel
    {
        [Display(Name = "Judge ID")]
        public int JudgeID { get; set; }
        [Display(Name = "Judge Name")]
        public string JudgeName { get; set; }
        [Display(Name = "Salutation")]
        public string Salutation { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddr { get; set; }
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
    }
}
