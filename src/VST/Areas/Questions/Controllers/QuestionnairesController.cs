using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VST.Areas.Questions.Models;
using System.Data.Entity;

namespace VST.Areas.Questions.Controllers
{
    public class QuestionnairesController : Controller
    {
        // GET: Questions/Questionnaires
        public ActionResult Index()
        {
            VTSDBEntities objDB = new VTSDBEntities();
            //var aa = objDB.Question1.Include(a => a.Option1).Include(b => b.Answer1);
            //foreach (Question1 qa in aa)
            //{
            //    var ans = qa.Answer1;
            //}
            List<Question1> questions1 = (from quesiton1 in objDB.Question1 select quesiton1).ToList();
            List<Question2> questions2 = (from quesiton2 in objDB.Question2 select quesiton2).ToList();
            List<Question3> questions3 = (from quesiton3 in objDB.Question3 select quesiton3).ToList();
            List<Question4> questions4 = (from quesiton4 in objDB.Question4 select quesiton4).ToList();
            List<Question5> questions5 = (from quesiton5 in objDB.Question5 select quesiton5).ToList();
            //ViewBag.Questions2 = from question2 in objDB.Question2 select question2;
            //ViewBag.Option1 = from option1 in objDB.Option1 select option1;
            //ViewBag.Answer1 = from answer1 in objDB.Answer1 select answer1;

            foreach (Question1 ques1 in questions1)
            {
                ques1.Option1 = (from option in objDB.Option1 where option.QuestionID == ques1.QuestionID select option).ToList();
                ques1.Answer1 = (from answer in objDB.Answer1 where answer.QuestionID == ques1.QuestionID select answer).ToList();
            }
            foreach (Question2 ques2 in questions2)
            {
                ques2.Option2 = (from option in objDB.Option2 where option.QuestionID == ques2.QuestionID select option).ToList();
                ques2.Answer2 = (from answer in objDB.Answer2 where answer.QuestionID == ques2.QuestionID select answer).ToList();
            }
            foreach (Question3 ques3 in questions3)
            {
                ques3.Answer3 = (from answer in objDB.Answer3 where answer.QuestionID == ques3.QuestionID select answer).ToList();
            }
            foreach (Question4 ques4 in questions4)
            {
                ques4.Answer4 = (from answer in objDB.Answer4 where answer.QuestionID == ques4.QuestionID select answer).ToList();
            }
            foreach (Question5 ques5 in questions5)
            {
                ques5.Answer5 = (from answer in objDB.Answer5 where answer.QuestionID == ques5.QuestionID select answer).ToList();
            }
            ViewData["Questions1"] = questions1;
            ViewData["Questions2"] = questions2;
            ViewData["Questions3"] = questions3;
            ViewData["Questions4"] = questions4;
            ViewData["Questions5"] = questions5;
            return View();
        }

        [HttpGet]
        public ActionResult AddQuestionnaire()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestionnaire(QuestionnairesViewModel Model, string[] BlankOptions, string[] AnswerOptions)
        {
            try
            {
                VTSDBEntities objDB = new VTSDBEntities();

                if (Model.QuestionType == DAL.Model.QuestionType.SingleLine)
                {
                    Question1 question1 = new Question1();
                    question1.QuestionSentence = Model.QuestionText;
                    question1.OriginalSentence = "";
                    List<Option1> QuesOptions = new List<Option1>();
                    foreach (string OptionText in BlankOptions)
                    {
                        Option1 EachOption = new Option1();
                        EachOption.OptionText = OptionText;
                        EachOption.Question1 = question1;
                        QuesOptions.Add(EachOption);
                    }

                    List<Answer1> AllAnswers = new List<Answer1>();
                    foreach (string AnswerText in AnswerOptions)
                    {
                        Answer1 EachAnswer = new Answer1();
                        EachAnswer.AnswerText = AnswerText;
                        EachAnswer.Question1 = question1;
                        AllAnswers.Add(EachAnswer);
                    }

                    question1.Answer1 = AllAnswers;
                    question1.Option1 = QuesOptions;
                    objDB.Question1.Add(question1);
                    objDB.SaveChanges();
                    ViewBag.SuccessMsg = "Questionnaire added successfully.";
                }
                else if (Model.QuestionType == DAL.Model.QuestionType.MultiLine)
                {
                    Question2 question2 = new Question2();
                    question2.QuestionText = Model.QuestionText;

                    List<Option2> QuesOptions = new List<Option2>();
                    foreach (string OptionText in BlankOptions)
                    {
                        Option2 EachOption = new Option2();
                        EachOption.OptionText = OptionText;
                        EachOption.Question2 = question2;
                        QuesOptions.Add(EachOption);
                    }

                    List<Answer2> AllAnswers = new List<Answer2>();
                    Answer2 EachAnswer = new Answer2();
                    EachAnswer.Answer = Model.AnswerText;
                    EachAnswer.Question2 = question2;
                    AllAnswers.Add(EachAnswer);


                    question2.Answer2 = AllAnswers;
                    question2.Option2 = QuesOptions;
                    objDB.Question2.Add(question2);
                    objDB.SaveChanges();
                    ViewBag.SuccessMsg = "Questionnaire added successfully.";
                }
                else if (Model.QuestionType == DAL.Model.QuestionType.SingleOption)
                {
                    Question3 question3 = new Question3();
                    question3.QuestionText = Model.QuestionText;

                    List<Answer3> AllAnswers = new List<Answer3>();
                    foreach (string AnswerText in AnswerOptions)
                    {
                        Answer3 EachAnswer = new Answer3();
                        EachAnswer.Answer = AnswerText;
                        EachAnswer.Question3 = question3;
                        AllAnswers.Add(EachAnswer);
                    }


                    question3.Answer3 = AllAnswers;
                    objDB.Question3.Add(question3);
                    objDB.SaveChanges();
                    ViewBag.SuccessMsg = "Questionnaire added successfully.";
                }
                else if (Model.QuestionType == DAL.Model.QuestionType.MultiOption)
                {
                    Question4 question4 = new Question4();
                    question4.QuestionText = Model.QuestionText;

                    List<Answer4> AllAnswers = new List<Answer4>();
                    foreach (string AnswerText in AnswerOptions)
                    {
                        Answer4 EachAnswer = new Answer4();
                        EachAnswer.Answer = AnswerText;
                        EachAnswer.Question4 = question4;
                        AllAnswers.Add(EachAnswer);
                    }

                    question4.Answer4 = AllAnswers;
                    objDB.Question4.Add(question4);
                    objDB.SaveChanges();
                    ViewBag.SuccessMsg = "Questionnaire added successfully.";
                }
                else if (Model.QuestionType == DAL.Model.QuestionType.Date)
                {
                    Question5 question5 = new Question5();
                    question5.QuestionText = Model.QuestionText;

                    List<Answer5> AllAnswers = new List<Answer5>();
                    foreach (string AnswerText in AnswerOptions)
                    {
                        Answer5 EachAnswer = new Answer5();
                        EachAnswer.Answer = AnswerText;
                        EachAnswer.Question5 = question5;
                        AllAnswers.Add(EachAnswer);
                    }

                    question5.Answer5 = AllAnswers;
                    objDB.Question5.Add(question5);
                    objDB.SaveChanges();
                    ViewBag.SuccessMsg = "Questionnaire added successfully.";
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                //throw raise;
                ViewBag.ErrorMsg = "An error occurred while saving the questionnaire.";
            }

            ModelState.Clear();
            return View();
        }
    }
}