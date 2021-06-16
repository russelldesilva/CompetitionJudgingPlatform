using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int CompetitionID { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTimePosted { get; set; }
    }
}
