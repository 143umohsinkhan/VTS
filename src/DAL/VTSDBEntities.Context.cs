﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VTSDBEntities : DbContext
    {
        public VTSDBEntities()
            : base("name=VTSDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VTSUSER> VTSUSERs { get; set; }
        public virtual DbSet<Answer1> Answer1 { get; set; }
        public virtual DbSet<Answer2> Answer2 { get; set; }
        public virtual DbSet<Option1> Option1 { get; set; }
        public virtual DbSet<Option2> Option2 { get; set; }
        public virtual DbSet<Question1> Question1 { get; set; }
        public virtual DbSet<Question2> Question2 { get; set; }
        public virtual DbSet<TestPaper> TestPapers { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<UserTest> UserTests { get; set; }
        public virtual DbSet<UserTestAnswer> UserTestAnswers { get; set; }
        public virtual DbSet<Answer3> Answer3 { get; set; }
        public virtual DbSet<Answer4> Answer4 { get; set; }
        public virtual DbSet<Answer5> Answer5 { get; set; }
        public virtual DbSet<Question3> Question3 { get; set; }
        public virtual DbSet<Question4> Question4 { get; set; }
        public virtual DbSet<Question5> Question5 { get; set; }
    }
}
