using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VST.Areas.Questions.Models
{   
    public class QuestionnairesViewModel
    {
        [Required]
        [Display(Name="Question Title")]
        public string QuestionText { get; set; }

        [Required]
        [Display(Name = "Question Type")]
        public QuestionType QuestionType { get; set; }

        [Required]
        [Display(Name = "Required")]
        public bool Required { get; set; }

        public List<QuestionOption> QuestionOptions { get; set; }

        [Display(Name = "Correct Answer")]
        public string AnswerText { get; set; }

        public List<Int64> SelectedOptions { get; set; }
    }

    public class QuestionOption
    {
        public Int64 OptionID;
        public string OptionTitle;
        public Int64 QuestionID;

    }
}