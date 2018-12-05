using DAL;
using DAL.Model;
using DAL.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;
using Utility;

namespace VST.Areas.Questions.Controllers
{
    public class TestController : Controller
    {
        VTSDBEntities ctx = new VTSDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TestPaper paper)
        {
            if (ModelState.IsValid)
            {
                paper.IsActive = true;
                paper.CreatedOn = DateTime.Now;
                ctx.TestPapers.Add(paper);
                ctx.SaveChanges();
                return RedirectToAction("ViewAllTestPapers");
            }
            return View(paper);
        }

        public ActionResult ViewAllTestPapers()
        {
            return View(ctx.TestPapers.ToList());
        }

        public ActionResult TestQuestions(int testPaperID = 0)
        {
            ViewBag.TestID = testPaperID;
            return View();
        }

        public ActionResult ViewAllQuestions(int QuestionType = 0, int TestID = 0)
        {
            if (QuestionType == 0) return View();
            var questionManager = new TestQuestionManager();
            var response = questionManager.ToQuestionViewModel(questionManager.GetQuestionsByType(QuestionType), QuestionType);
            response.TestID = TestID;
            return PartialView("ViewAllQuestions", response);
        }

        public ActionResult AddQuestion(int testId = 0, int QuestionID = 0, int QuestionType = 0)
        {
            var questionManager = new TestQuestionManager();
            var res = questionManager.AddQuestionInTestPaper(testId, QuestionID, QuestionType);
            if (res == TestQuestionManager.QuestionAddToTestPaperResponse.Exist)
                ViewBag.AddQuestionToTestPaperResponse = "Question alread exist in test paper";
            else if (res == TestQuestionManager.QuestionAddToTestPaperResponse.Sucess)
                ViewBag.AddQuestionToTestPaperResponse = "Question add";
            else
                ViewBag.AddQuestionToTestPaperResponse = "Failed to add";
            return RedirectToAction("TestQuestions", new { testPaperID = testId });
        }

        public ActionResult InitTest(int testPaperID = 0)
        {
            if (Session[VTSConstants.UserID] == null) return RedirectToAction("ForceSignOut", "../Account");
            var userID = Convert.ToInt64(Session[VTSConstants.UserID]);
            var question = new ExamManager().StartTest(userID, testPaperID);
            return View(UpdateQuestion(question, 0));
        }

        public ActionResult Next()
        {
            var question = (ExamViewModel)TempData[VTSConstants.QuestionKey];
            question.QuestionIndex = ++question.QuestionIndex;
            return View("InitTest", UpdateQuestion(question, question.QuestionIndex));
        }

        public ActionResult ViewAllQuestionsofTestPaper(int testPaperID = 0)
        {
            return View(new TestQuestionManager().GetAllQuestionsFromTestQuestionByTestID(testPaperID));
        }

        public ActionResult STPU()
        {
            return View(ctx.TestPapers.ToList());
        }

        public ActionResult Previous()
        {
            var question = (ExamViewModel)TempData[VTSConstants.QuestionKey];
            question.QuestionIndex = --question.QuestionIndex;
            return View("InitTest", UpdateQuestion(question, question.QuestionIndex));
        }

        //public ActionResult SubmitAnswer()
        //{ }

        //public ActionResult FinishExam()
        //{ }

        public ActionResult GetPartialViewByQuestionType(IQuestion question)
        {
            switch (question.Type)
            {
                case QuestionType.SingleLine:
                    {
                        return PartialView("_Question1View", question);
                    }
                case QuestionType.MultiOption:
                    {
                        return PartialView("_Question2View", question);
                    }
                default:
                    {
                        return PartialView();
                    }
            }
        }

        public string GetPartialViewNameByType(QuestionType type)
        {
            switch (type)
            {
                case QuestionType.SingleLine:
                    {
                        return "_Question1View";
                    }
                case QuestionType.MultiLine:
                    {
                        return "_Question2View";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        void UpdateChache(ExamViewModel question)
        {
            if (!TempData.ContainsKey(VTSConstants.QuestionKey))
                TempData.Add(VTSConstants.QuestionKey, question);
            TempData[VTSConstants.QuestionKey] = question;
            TempData.Keep(VTSConstants.QuestionKey);
        }

        public ExamViewModel UpdateQuestion(ExamViewModel question, int index)
        {
            if (question == null || question.Questions.Count <= 0 || index > question.Questions.Count - 1)
            { UpdateChache(question); return new ExamViewModel(); }

            question.CurrentQuestion = question.Questions[index];
            ViewBag.PartialName = GetPartialViewNameByType(question.CurrentQuestion.Type);
            UpdateChache(question);
            return question;
        }
    }
}