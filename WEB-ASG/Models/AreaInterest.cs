using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB_ASG.Models
{
    public class AreaInterest
    {
        public int AreaInterestID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<Judge> JudgeList { get; set; }
        public void AddJudge(Judge j)
        {
            JudgeList.Add(j);
        }
        public void RemoveJudge(Judge j)
        {
            JudgeList.Remove(j);
        }
    }
}
