using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CriteriaViewModel
    {
        public int CriteriaID { get; set; }
        [Required]
        [StringLength(50)]
        public string CriteriaName { get; set; }
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
        [Display(Name = "Competition Name")]
        public string CompetitionName { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "The field {0} must be greater than 1 or lesser than 100.")]
        [DefaultValue(0)]
        [ValidateWeightage]
        public int Weightage { get; set; }


    }
}
