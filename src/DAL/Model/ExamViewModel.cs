using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class ExamViewModel
    {
        private DetailQuestionViewModel questions;
        public DetailQuestionViewModel Questions
        {
            get { return questions; }
            set
            {
                questions = value;
                if (value != null && value.Count > 0)
                {
                    CurrentQuestion = value.FirstOrDefault();
                }
            }
        }

        public int UserTestID { get; set; }
        public IQuestion CurrentQuestion { get; set; }
        public int QuestionIndex { get; set; }

        public ExamViewModel()
        {
            Questions = new DetailQuestionViewModel();
        }
    }
}
