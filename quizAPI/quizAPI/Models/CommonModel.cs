using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quizAPI.Models
{
    public class CommonModel
    {
    }

    public class listModel
    {
        public int ID { get; set; }
        public string listName { get; set; }
        public string UserId { get; set; }
        public bool Status { get; set; }
    }

    ///<summary>
    ///quizModel refers to quizzes.
    ///</summary>
    public class quizModel
    {
        ///<summary>
        ///ID of the quiz.
        ///</summary>
        public int ID { get; set; }
        ///<summary>
        ///Name of the quiz.
        ///</summary>
        public string QuizName { get; set; }
        ///<summary>
        ///Image of the quiz.
        ///</summary>
        public string Image { get; set; }
        ///<summary>
        ///Description of the quiz.
        ///</summary>
        public string Description { get; set; }
        ///<summary>
        ///What type the quiz is, such as maths or english.
        ///</summary>
        public string QuizType { get; set; }
        ///<summary>
        ///whether the quiz is live or not.
        ///</summary>
        public bool Status { get; set; }
    }


    ///<summary>
    ///questionModel refers to questions.
    ///</summary>
    public class questionModel
    {
        ///<summary>
        ///ID of the question.
        ///</summary>
        public int ID { get; set; }
        ///<summary>
        ///Which quiz the question belongs to.
        ///</summary>
        public int Quiz_ID { get; set; }
        ///<summary>
        ///Question number  that's being selected.
        ///</summary>
        public byte QuestionNumber { get; set; }
        ///<summary>
        ///Text of the question.
        ///</summary>
        public string QuestionText { get; set; }
        ///<summary>
        ///Text of the first answer.
        ///</summary>
        public string FirstAnswer { get; set; }
        ///<summary>
        ///Text of the second answer.
        ///</summary>
        public string SecondAnswer { get; set; }
        ///<summary>
        ///Text of the third answer.
        ///</summary>
        public string ThirdAnswer { get; set; }
        ///<summary>
        ///Text of the fourth answer.
        ///</summary>
        public string FourthAnswer { get; set; }
        ///<summary>
        ///whether the first answer is correct/true or incorrect/false.
        ///</summary>
        public bool IsFirstAnswerCorrect { get; set; }
        ///<summary>
        ///whether the second answer is correct/true or incorrect/false.
        ///</summary>
        public bool IsSecondAnswerCorrect { get; set; }
        ///<summary>
        ///whether the third answer is correct/true or incorrect/false.
        ///</summary>
        public bool IsThirdAnswerCorrect { get; set; }
        ///<summary>
        ///whether the fourth answer is correct/true or incorrect/false.
        ///</summary>
        public bool isFourthAnswerCorrect { get; set; }
        ///<summary>
        ///If the question is live or not.
        ///</summary>
        public bool Status { get; set; }

    }
}   