using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.DAL;

namespace WEB_ASG.Models
{
    public class ValidateWeightage : ValidationAttribute
    {
        private JudgeDAL judgeContext = new JudgeDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Casting the validation context to the "Staff" model class
            Criteria criteria = (Criteria)validationContext.ObjectInstance;
            int weightage = Convert.ToInt32(value);
            
            // Get the Staff Id from the staff instance
            int competitionID = criteria.CompetitionID;
            int criteriaID = criteria.CriteriaID;
            if (judgeContext.IsWeightageRight(competitionID, weightage, criteriaID))
                // validation failed
                return new ValidationResult
                ("Weightage is above 100!");
            else
                // validation passed
                return ValidationResult.Success;
        }
    }

}
