using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CompetitionSubmissionViewModel
    {
        [Display(Name = "Competition ID")]
        public int CompetitionID { get; set; }
        [Display(Name = "Competition")]
        public string CompetitionName { get; set; }
        [Display(Name = "Competitor ID")]
        public int CompetitorID { get; set; }
        [Display(Name = "Upload Work")]
        public IFormFile fileToUpload { get; set; }
        [StringLength(255, ErrorMessage = "Filename can only have a maximum of 255 characters!")]
        [Display(Name = "Uploaded Work")]
        public string FileSubmitted { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date File Uploaded")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime? DateTimeFileUpload { get; set; }
        [StringLength(255, ErrorMessage = "Appeal can only have a maximum of 255 characters!")]
        public string Appeal { get; set; }
    }
}
