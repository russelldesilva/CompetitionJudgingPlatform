using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class Competitor
    {
        public int CompetitorID { get; set; }
        [Required, StringLength(5)]
        public string CompetitorName { get; set; }
        [Required, StringLength(5)]
        public string Salutation { get; set; }
        [Required, EmailAddress, StringLength(50), ValidateEmailExists]
        public string EmailAddr { get; set; }
        [Required, StringLength(255), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
