using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBQCode.Web.Models
{
    public class ContactUsModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string DoNotFillThis { get; set; }
        public int QuestionPartOne { get; set; }
        public int QuestionPartTwo { get; set; }
        public int? Answer { get; set; }
        public string Question { get { return String.Format("{0} + {1} =", QuestionPartOne, QuestionPartTwo); } }
    }
}