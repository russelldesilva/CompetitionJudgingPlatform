using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class Competitor
    {
        [Display(Name = "Competitor ID")]
        public int CompetitorID { get; set; }
        [Required, StringLength(5)]
        [Display(Name = "Competitor Name")]
        public string CompetitorName { get; set; }
        [Required, StringLength(5)]
        [Display(Name = "Salulation")]
        public string Salutation { get; set; }
        [Required, EmailAddress, StringLength(50), ValidateEmailExists]
        [Display(Name = "Email")]
        public string EmailAddr { get; set; }
        [Required, StringLength(255), DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
