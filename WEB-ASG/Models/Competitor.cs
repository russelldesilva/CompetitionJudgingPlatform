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
        public int CompetitionID { get; set; }
        [Required]
        [StringLength(50)]
        public int CompetitorName { get; set; }
        [StringLength(5)]
        public string Salutation { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$")]
        [StringLength(50)]
        public string EmailAddr { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
