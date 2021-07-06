using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class CompetitionSubmission
    {
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
        [Display(Name = "Competitor ID")]
        public int CompetitorID { get; set; }
        [Display(Name = "File name")]
        [StringLength(255, ErrorMessage = "File can only have a maximum of 255 characters!"), ValidateFileFormat]
        public string FileSubmitted { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateTimeFileUpload { get; set; }
        [Display(Name = "Appeal")]
        [StringLength(255, ErrorMessage = "File can only have a maximum of 255 characters!")]
        public string Appeal { get; set; }
        [Display(Name = "Vote Count")]
        public int VoteCount { get; set; }
        [Display(Name = "Ranking")]
        public int? Ranking { get; set; }
    }
}
