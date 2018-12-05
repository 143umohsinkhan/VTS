using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [MetadataType(typeof(UserMetaData))]
    public partial class VTSUSER
    {
        public VTSUSER()
        {
            Country = "India";
            this.UserTests = new HashSet<UserTest>();
            this.UserTestAnswers = new HashSet<UserTestAnswer>();
            this.UserTests = new HashSet<UserTest>();
            this.UserTestAnswers = new HashSet<UserTestAnswer>();
        }
    }

    [MetadataType(typeof(TestPaperMetaData))]
    public partial class TestPaper
    {
    }

    public interface IQuestion
    {
        long QuestionID { get; set; }
        QuestionType Type { get; }
    }

    public partial class Question1 : IQuestion
    {
        public QuestionType Type
        {
            get
            {
                return QuestionType.SingleLine;
            }
        }
    }
    public partial class Question2 : IQuestion
    {
        public QuestionType Type
        {
            get
            {
                return QuestionType.MultiLine;
            }
        }
    }

    public partial class Question3 : IQuestion
    {
        public QuestionType Type
        {
            get
            {
                return QuestionType.SingleOption;
            }
        }
    }

    public partial class Question4 : IQuestion
    {
        public QuestionType Type
        {
            get
            {
                return QuestionType.MultiOption;
            }
        }
    }

    public partial class Question5 : IQuestion
    {
        public QuestionType Type
        {
            get
            {
                return QuestionType.Date;
            }
        }
    }
}
