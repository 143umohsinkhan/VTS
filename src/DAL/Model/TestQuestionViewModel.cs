using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class TestQuestionViewModel
    {
        public string QuestionName { get; set; }
        public long QuetionID { get; set; }
        public string QuestionType { get; set; }
        public int TestPaperID { get; set; }
    }

    public class QuestionViewModel
    {
        public string QuestionText { get; set; }
        public long QuestionID { get; set; }
        public int QuestionType { get; set; }
        public string QuestionTypeText
        {
            get
            {
                if (QuestionType > 0)
                {
                    switch ((QuestionType)QuestionType)
                    {
                        case Model.QuestionType.SingleLine:
                            return "Single Line";
                        case Model.QuestionType.MultiLine:
                            return "Multi Line";
                        default:
                            return string.Empty;
                    }
                }
                return string.Empty;
            }
        }
    }

    public class DetailQuestionViewModel : List<IQuestion>
    {
    }

    public class QuestionViewModels
    {
        public int TestID { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public QuestionViewModels()
        {
            Questions = new List<QuestionViewModel>();
        }
    }
}