using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CompetitionScore
    {
        public int CriteriaID { get; set; }
        public int CompetitorID { get; set; }
        public int CompetitionID { get; set; }
        [DefaultValue(0)]
        [Range(0, 10, ErrorMessage = "The field {0} must be between 0 and 10.")]
        public int Score { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateTimeLastEdit { get; set; }
    }
}
