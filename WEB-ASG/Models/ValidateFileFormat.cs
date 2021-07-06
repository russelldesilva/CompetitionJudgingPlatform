using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace WEB_ASG.Models
{
    public class ValidateFileFormat : ValidationAttribute
    {
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            string file = Convert.ToString(value);
            if (Regex.IsMatch(file, @"^[File]+_+([1-9]|[1-9][0-9]|100)+_+([1-9]|[1-9][0-9]|100)+(.doc|.docx|.pdf|.png|.jpg|.gif|.txt)$") || file == "")
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("File must be in following format 'File_CompetitorID_CompetitionID.FileType'. File type can only be in .doc/.docx/.pdf/.png/.jpg/.gif/.txt");
            }
        }
    }
}
