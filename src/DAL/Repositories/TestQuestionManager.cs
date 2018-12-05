using DAL.Model;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class TestQuestionManager
    {
        VTSDBEntities ctx = new VTSDBEntities();

        public List<object> GetQuestionsByType(int type)
        {
            switch ((QuestionType)type)
            {
                case QuestionType.SingleLine:
                    {
                        return ctx.Question1.ToList<object>();
                    }
                case QuestionType.MultiLine:
                    {
                        return ctx.Question2.ToList<object>();
                    }
                case QuestionType.SingleOption:
                    {
                        return ctx.Question3.ToList<object>();
                    }
                case QuestionType.MultiOption:
                    {
                        return ctx.Question4.ToList<object>();
                    }
                case QuestionType.Date:
                    {
                        return ctx.Question5.ToList<object>();
                    }
                default:
                    {
                        return new List<object>();
                    }
            }
        }

        public object GetQuestionsById(int type, long Questionid)
        {
            switch ((QuestionType)type)
            {
                case QuestionType.SingleLine:
                    {
                        return ctx.Question1.Include("Answer1").Include("Option1").Where(x => x.QuestionID == Questionid).FirstOrDefault();
                    }
                case QuestionType.MultiLine:
                    {
                        var obj = ctx.Question2.Include("Option2").Include("Answer2").Where(x => x.QuestionID == Questionid).FirstOrDefault();
                        ctx.Entry(obj).Collection(i => i.Option2).Load();
                        return obj;
                    }
                case QuestionType.SingleOption:
                    {
                        return ctx.Question3.Include("Answer3").Where(x => x.QuestionID == Questionid).FirstOrDefault();
                    }
                case QuestionType.MultiOption:
                    {
                        return ctx.Question4.Include("Answer4").Where(x => x.QuestionID == Questionid).FirstOrDefault();
                    }
                case QuestionType.Date:
                    {
                        return ctx.Question5.Include("Answer5").Where(x => x.QuestionID == Questionid).FirstOrDefault();
                    }
                default:
                    {
                        return new List<object>();
                    }
            }
        }

        //public List<object> GetQuestionsByTestPaperID(int testPaperID)
        //{
        //    var getTestPaperQuestions = ctx.TestQuestions.Where(x => x.Id == testPaperID);
        //    return getTestPaperQuestions.ToList<object>();
        //}

        public List<TestQuestion> GetTestQuestionByTestPaperID(int testPaperID)
        {
            return ctx.TestQuestions.Where(x => x.TestPaperID == testPaperID).ToList();
        }

        public List<IQuestion> GetQuestionByTestPaperID(int testPaperID)
        {
            List<IQuestion> questions = new List<IQuestion>();
            var testQuestions = GetTestQuestionByTestPaperID(testPaperID);

            foreach (var item in testQuestions)
            {
                questions.Add((IQuestion)GetQuestionsById(Convert.ToInt32(item.QuetionType), item.QuestionID));
            }
            return questions;
        }

        public QuestionViewModels ToQuestionViewModel(List<object> value, int type)
        {
            switch ((QuestionType)type)
            {
                case QuestionType.SingleLine:
                    {
                        return CreateQuestion1Lists(value);
                    }
                case QuestionType.MultiLine:
                    {
                        return CreateQuestion2Lists(value);
                    }
                case QuestionType.SingleOption:
                    {
                        return CreateQuestion3Lists(value);
                    }
                case QuestionType.MultiOption:
                    {
                        return CreateQuestion4Lists(value);
                    }
                case QuestionType.Date:
                    {
                        return CreateQuestion5Lists(value);
                    }
                default:
                    {
                        return new QuestionViewModels();
                    }
            }
        }

        public List<QuestionViewModel> GetAllQuestionsFromTestQuestionByTestID(int testId)
        {
            var questions = GetQuestionByTestPaperID(testId);
            var model = new List<QuestionViewModel>();
            foreach (var item in questions)
            {
                model.Add(ConvertToType(item, item.Type));
            }
            return model;
        }

        QuestionViewModels CreateQuestion1Lists(List<object> values)
        {
            QuestionViewModels obj = new QuestionViewModels();
            foreach (var item in values)
            {
                obj.Questions.Add(ConvertToQuestion1(item));
            }
            return obj;
        }

        public QuestionViewModel ConvertToType(object question, QuestionType questionType)
        {
            switch (questionType)
            {
                case QuestionType.SingleLine:
                    return ConvertToQuestion1(question);
                case QuestionType.MultiLine:
                    return ConvertToQuestion2(question);
                case QuestionType.SingleOption:
                    return ConvertToQuestion3(question);
                case QuestionType.MultiOption:
                    return ConvertToQuestion4(question);
                case QuestionType.Date:
                    return ConvertToQuestion5(question);
                default:
                    return null;
            }
        }

        QuestionViewModels CreateQuestion2Lists(List<object> values)
        {
            QuestionViewModels obj = new QuestionViewModels();
            foreach (var item in values)
            {
                obj.Questions.Add(ConvertToQuestion2(item));
            }
            return obj;
        }

        QuestionViewModels CreateQuestion3Lists(List<object> values)
        {
            QuestionViewModels obj = new QuestionViewModels();
            foreach (var item in values)
            {
                obj.Questions.Add(ConvertToQuestion3(item));
            }
            return obj;
        }

        QuestionViewModels CreateQuestion4Lists(List<object> values)
        {
            QuestionViewModels obj = new QuestionViewModels();
            foreach (var item in values)
            {
                obj.Questions.Add(ConvertToQuestion4(item));
            }
            return obj;
        }

        QuestionViewModels CreateQuestion5Lists(List<object> values)
        {
            QuestionViewModels obj = new QuestionViewModels();
            foreach (var item in values)
            {
                obj.Questions.Add(ConvertToQuestion5(item));
            }
            return obj;
        }

        QuestionViewModel ConvertToQuestion1(object value)
        {
            var question1 = (Question1)value;
            return new QuestionViewModel { QuestionID = question1.QuestionID, QuestionText = question1.QuestionSentence, QuestionType = Convert.ToInt32(QuestionType.SingleLine) };
        }

        QuestionViewModel ConvertToQuestion2(object value)
        {
            var question1 = (Question2)value;
            return new QuestionViewModel { QuestionID = question1.QuestionID, QuestionText = question1.QuestionText, QuestionType = Convert.ToInt32(QuestionType.MultiLine) };
        }

        QuestionViewModel ConvertToQuestion3(object value)
        {
            var question1 = (Question3)value;
            return new QuestionViewModel { QuestionID = question1.QuestionID, QuestionText = question1.QuestionText, QuestionType = Convert.ToInt32(QuestionType.SingleOption) };
        }

        QuestionViewModel ConvertToQuestion4(object value)
        {
            var question1 = (Question4)value;
            return new QuestionViewModel { QuestionID = question1.QuestionID, QuestionText = question1.QuestionText, QuestionType = Convert.ToInt32(QuestionType.MultiOption) };
        }

        QuestionViewModel ConvertToQuestion5(object value)
        {
            var question1 = (Question5)value;
            return new QuestionViewModel { QuestionID = question1.QuestionID, QuestionText = question1.QuestionText, QuestionType = Convert.ToInt32(QuestionType.Date) };
        }

        public QuestionAddToTestPaperResponse AddQuestionInTestPaper(int testId, int QuestionID, int QuestionType)
        {
            try
            {
                var exist = ctx.TestQuestions.Where(x => x.TestPaperID == testId && x.QuestionID == QuestionID && x.QuetionType == QuestionType.ToString()).ToList();
                if (exist != null && exist.Count > 0) return QuestionAddToTestPaperResponse.Exist;

                ctx.TestQuestions.Add(new TestQuestion { QuestionID = QuestionID, TestPaperID = testId, QuetionType = Convert.ToString(QuestionType) });
                ctx.SaveChanges();
                return QuestionAddToTestPaperResponse.Sucess;
            }
            catch (Exception ex)
            { return QuestionAddToTestPaperResponse.Failed; }
        }
        public enum QuestionAddToTestPaperResponse
        {
            Sucess = 1,
            Failed = 2,
            Exist = 3
        }
    }

    public static class Extensions
    {
        public static Type ConverToQuestionType(this object value, int type)
        {
            switch ((QuestionType)type)
            {
                case QuestionType.SingleLine:
                    {
                        return typeof(Question1);
                    }
                case QuestionType.MultiLine:
                    {
                        return typeof(Question2);
                    }
                case QuestionType.SingleOption:
                    {
                        return typeof(Question3);
                    }
                case QuestionType.MultiOption:
                    {
                        return typeof(Question4);
                    }
                case QuestionType.Date:
                    {
                        return typeof(Question5);
                    }
                default: { return null; }
            }
        }

        //public static Type ConverToQuestionType(this List<object> value, int type)
        //{
        //    switch ((QuestionType)type)
        //    {
        //        case QuestionType.SingleLine:
        //            {
        //                return typeof(Question1);
        //            }
        //        case QuestionType.MultiLine:
        //            {
        //                return typeof(Question2);
        //            }
        //        default: { return null; }
        //    }
        //}
    }
}
