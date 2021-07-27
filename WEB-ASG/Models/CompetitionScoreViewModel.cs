using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CompetitionScoreViewModel
    {
        [Display(Name = "Criteria ID")]
        public int CriteriaID { get; set; }
        [Display(Name = "Weightage")]
        public int Weightage { get; set; }
        [Display(Name = "Competitor Name")]
        public string CompetitorName { get; set; }
        [Display(Name = "Competition Name")]
        public string CompetitionName { get; set; }
        [Display(Name = "CriteriaName")]
        public string CriteriaName { get; set; }
        [Display(Name = "Competitor ID")]
        public int CompetitorID { get; set; }
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
        [Display(Name = "Score")]
        [Required]
        [DefaultValue(0)]
        [Range(0, 10, ErrorMessage = "The field {0} must be between 0 and 10.")]
        public int Score { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateTimeLastEdit { get; set; }
    }
}
