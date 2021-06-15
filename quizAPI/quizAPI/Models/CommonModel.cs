using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quizAPI.Models
{
    public class CommonModel
    {
    }

    public class quizModel
    {
        public int ID { get; set; }
        public string QuizName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string QuizType { get; set; }
        public bool Status { get; set; }
    }

    public class questionModel
    {
        public int ID { get; set; }
        public int Quiz_ID { get; set; }
        public byte QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string FirstAnswer { get; set; }
        public string SecondAnswer { get; set; }
        public string ThirdAnswer { get; set; }
        public string FourthAnswer { get; set; }
        public bool IsFirstAnswerCorrect { get; set; }
        public bool IsSecondAnswerCorrect { get; set; }
        public bool IsThirdAnswerCorrect { get; set; }
        public bool isFourthAnswerCorrect { get; set; }
        public bool Status { get; set; }

    }
}   