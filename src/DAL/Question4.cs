//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question4
    {
        public Question4()
        {
            this.Answer4 = new HashSet<Answer4>();
        }
    
        public long QuestionID { get; set; }
        public string QuestionText { get; set; }
    
        public virtual ICollection<Answer4> Answer4 { get; set; }
    }
}