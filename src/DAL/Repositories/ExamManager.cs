using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ExamManager
    {
        VTSDBEntities ctx = new VTSDBEntities();

        public ExamViewModel StartTest(long UserID, int TestID)
        {
            var userTestId = ctx.UserTests.Add(new UserTest { TestID = TestID, UserID = UserID });
            ctx.SaveChanges();
            var examModel = new ExamViewModel { UserTestID = userTestId.Id };
            examModel.Questions.AddRange(GetQuestionsByTestID(TestID));
            return examModel;
        }

        public List<IQuestion> GetQuestionsByTestID(int testId)
        {
            return new TestQuestionManager().GetQuestionByTestPaperID(testId);
        }
    }
}
