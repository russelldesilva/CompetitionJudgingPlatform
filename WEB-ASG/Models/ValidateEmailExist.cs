﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.DAL;

namespace WEB_ASG.Models
{
    public class ValidateEmailExists : ValidationAttribute
    {
        private CompetitorDAL competitorContext = new CompetitorDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Get the email value to validate
            string email = Convert.ToString(value);
            // Casting the validation context to the "Competitor" model class
            Competitor competitor = (Competitor)validationContext.ObjectInstance;
            // Get the Competitor Id from the competitor instance
            int competitorId = competitor.CompetitorID;
            if (competitorContext.IsEmailExist(email, competitorId))
                // validation failed
                return new ValidationResult
                ("Email address already exists!");
            else
                // validation passed
                return ValidationResult.Success;
        }
    }
}
