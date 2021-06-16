﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class Competition
    {
        public int CompetitionID { get; set; }
        public int AreaInterestID { get; set; }
        public List<Judge> JudgeList { get; set; }
        public List<Competitor> CompetitorList { get; set; }
        [Required]
        [StringLength(255)]
        public string CompetitionName { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ResultReleaseDate { get; set; }
        public List<Comment> CommentList { get; set; }
    }
}
