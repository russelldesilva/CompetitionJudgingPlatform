using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CompetitionDetailsViewModel
    {
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; } = 0;
        public string AreaInterest { get; set; }
        [Display(Name = "Competition Name")]
        public string CompetitionName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Results Release Date")]
        public DateTime ResultReleaseDate { get; set; }

    }
}
