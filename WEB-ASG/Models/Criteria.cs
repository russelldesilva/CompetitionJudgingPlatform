using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WEB_ASG.Models
{
    public class Criteria
    {
        public int CriteriaID { get; set; }
        public int CompetitionID { get; set; }
        [Required]
        [StringLength(50)]
        public string CriteriaName { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "The field {0} must be greater than 1 or lesser than 100.")]
        [DefaultValue(0)]
        [ValidateWeightage]
        public int Weightage { get; set; }
    }
}
