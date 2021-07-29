using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Models
{
    public class CompetitorCompetitionViewModel
    {
        public List<CompetitionDetailsViewModel> competitionList { get; set; }
        public List<CompetitionScoreViewModel> scoreList { get; set; }

        public CompetitorCompetitionViewModel()
        {
            competitionList = new List<CompetitionDetailsViewModel>();
            scoreList = new List<CompetitionScoreViewModel>();
        }
    }
}
