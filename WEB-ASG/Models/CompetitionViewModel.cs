using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CompetitionViewModel
    {
        public int areaInterestID { get; set; }
        public string areaInterestName { get; set; }
        public List<CompetitionDetailsViewModel> competitionList { get; set; }
        public List<JudgeViewModel> judgeVMList { get; set; }
        public List<Criteria> criteriaList { get; set; }
        public List<Comment> commentList { get; set; }
        public Comment postComment { get; set; }
        public List<CompetitionSubmissionViewModel> submissions { get; set; }
        public CompetitionViewModel()
        {
            competitionList = new List<CompetitionDetailsViewModel>();
            judgeVMList = new List<JudgeViewModel>();
            criteriaList = new List<Criteria>();
            commentList = new List<Comment>();
            postComment = new Comment();
            submissions = new List<CompetitionSubmissionViewModel>();
        }
    }
}
