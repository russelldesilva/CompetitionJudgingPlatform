using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CompetitionJudge
    {
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
        [Display(Name = "Judge ID")]
        public int JudgeID { get; set; }
    }
}
