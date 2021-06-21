using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using quizAPI.Models;
using System.Web.Http.Cors;

namespace quizAPI.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class QuizController : ApiController
    {
        jackbenn_BennettoEntities db = new jackbenn_BennettoEntities();

        ///<summary>
        ///Gets quizzes regardless of status or whether it has enough questions. This is for admins.  
        ///</summary>
        ///<remarks>
        ///
        ///</remarks>
        ///<returns>
        ///Returns result from LINQ.
        ///</returns> 
        [Route("api/quiz/getQuizzes")]
        [HttpGet]
        public IHttpActionResult getQuizzes()
        {
            var result = db.Quiz_Table.Where(d => d.Status == true).ToList();
            return Ok(result);
        }

        ///<summary>
        ///Gets only the completed quizzes a total question count of 10 
        ///</summary>
        ///<remarks>
        ///Checks to see if status is true, and also that the quizzes have a total of 10 questions.
        ///</remarks>
        ///<returns>
        ///Returns result from LINQ.
        ///</returns> 
        [Route("api/quiz/getCompletedQuizzes")]
        [HttpGet]
        public IHttpActionResult getCompletedQuizzes()
        {
            var results = db.Quiz_Table.Where(d => d.Status == true).ToList();
            var empty = db.Quiz_Table.Where(d => d.Status == true).ToList();
            empty.Clear();
            foreach (var result in results)
            {
                var questions = db.Questions_Table.Where(d => d.Quiz_ID == result.ID).ToList();
                if(questions.Count >= 10)
                {
                    empty.Add(result);
                }
            }
            results = empty;
            return Ok(results);
        }



        ///<summary>
        ///Grabs a quiz
        ///</summary>
        ///<param ID = "ID" >ID of the quiz.</param>
        ///<remarks>
        ///
        ///</remarks>
        ///<returns>
        ///Returns result from LINQ.
        ///</returns> 
        [Route("api/quiz/getQuiz")]
        [HttpGet]
        public IHttpActionResult getQuiz(int ID)
        {
            var result = db.Quiz_Table.Where(d => d.ID == ID).ToList();
            return Ok(result);
        }



        ///<summary>
        ///Grabs the question data that the user has selected..
        ///</summary>
        ///<param QuizId = "QuizId" >Uses the quizId that the user is looking at.</param>
        ///<param questionNumber = "questionNumber" >Uses the question number as a parameter given by the user.</param>
        ///<remarks>
        ///
        ///</remarks>
        ///<returns>
        ///Returns result from LINQ.
        ///</returns> 
        [Route("api/quiz/getQuestionData")]
        [HttpGet]
        public IHttpActionResult getQuestionData(int QuizId, int questionNumber)
        {
            var result = db.Questions_Table.Where(d => d.Quiz_ID == QuizId && d.QuestionNumber == questionNumber).ToList();
            return Ok(result);
        }



        ///<summary>
        ///Checks to see if answer is correct.
        ///</summary>
        ///<param QuizId = "QuizId" >ID of the quiz from the answered question.</param>
        ///<param QuestionNumber = "QuestionNumber" >Question Number of the quiz.</param>
        ///<param IsFirstAnswerCorrect = "IsFirstAnswerCorrect" >User selected first answer as true.</param>
        ///<param IsSecondAnswerCorrect = "IsSecondAnswerCorrect" >User selected second answer as true.</param>
        ///<param IsThirdAnswerCorrect = "IsThirdAnswerCorrect" >User selected third answer as true.</param>
        ///<param isFourthAnswerCorrect = "isFourthAnswerCorrect" >User selected fourth answer as true.</param>
        ///<remarks>
        ///Checks all parameters from the question the student has answererd. 
        ///</remarks>
        ///<returns>
        ///If the LINQ has a count of 1 or more, that means that the answer is correct. Returns 1 if correct, returns 0 if incorrect. 
        ///</returns> 
        [Route("api/quiz/checkIfAnswerCorrect")]
        [HttpGet]
        public IHttpActionResult checkIfAnswerCorrect(int QuizId, int QuestionNumber, bool IsFirstAnswerCorrect, bool IsSecondAnswerCorrect, bool IsThirdAnswerCorrect, bool isFourthAnswerCorrect)
        {
            var result = db.Questions_Table.Where(d => d.Quiz_ID == QuizId && d.QuestionNumber == QuestionNumber && d.IsFirstAnswerCorrect == IsFirstAnswerCorrect && d.IsSecondAnswerCorrect == IsSecondAnswerCorrect && d.IsThirdAnswerCorrect == IsThirdAnswerCorrect && d.isFourthAnswerCorrect == isFourthAnswerCorrect).ToList();
            if(result.Count >= 1)
            {
                return Ok(1);
            } else
            {
                return Ok(0);
            }
        }




        ///<summary>
        ///Checks to see if given credentials are correct. If so, returns result from LINQ.
        ///</summary>
        ///<param Username = "Username" >The Username the user has entered.</param>
        ///<param Password = "Password" >The password the user has entered.</param>
        ///<remarks>
        ///
        ///</remarks>
        ///<returns>
        ///Returns result from LINQ.
        ///</returns> 
        [Route("api/quiz/getAccountDetails")]
        [HttpGet]
        public IHttpActionResult getAccountDetails(string Username, string Password)
        {
            var result = db.Account_Table.Where(d => d.Username == Username && d.Password == Password).ToList();
            return Ok(result);
        }




        ///<summary>
        ///Creates a new question, answer text, and answers which are either true of false.
        ///</summary>
        ///<param model = "model" >Uses the quizModel as a parameter.</param>
        ///<remarks>
        ///Checks to see if quiz name is live, and if there is a identical quiz name. If so, returns 0. 
        ///</remarks>
        ///<returns>
        ///Returns record ID if succeeded, 0 if failed.
        ///</returns> 
        [Route("api/quiz/createQuiz")]
        [HttpPost]
        public IHttpActionResult createQuiz(quizModel model)
        {
            try
            {
                var existingQuiz = db.Quiz_Table.FirstOrDefault(d => d.QuizName == model.QuizName & d.Status == true);

                if(existingQuiz == null)
                {
                    var record = new Quiz_Table();
                    record.QuizName = model.QuizName;
                    record.Description = model.Description;
                    record.Image = model.Image;
                    record.QuizType = model.QuizType;
                    record.Status = model.Status;

                    db.Quiz_Table.Add(record);
                    db.SaveChanges();
                    return Ok(record.ID);
                } else
                {
                    return Ok(0);
                }
            }
            catch { return Ok(0); }
        }



        ///<summary>
        ///Creates a new question, answer text, and answers which are either true of false.
        ///</summary>
        ///<param model = "model" >Uses the questionModel as a parameter.</param>
        ///<remarks>
        ///Checks to see if question is already created. If so, edits record, doesn't add record.
        ///</remarks>
        ///<returns>
        ///Returns record ID if succeeded, 0 if failed.
        ///</returns> 
        [Route("api/quiz/createQuestionAndAnswers")]
        [HttpPost]
        public IHttpActionResult createQuestionAndAnswers(questionModel model)
        {
            try
            {
                var record = db.Questions_Table.FirstOrDefault(d =>  d.Quiz_ID == model.Quiz_ID && d.QuestionNumber == model.QuestionNumber);
                if(record == null)
                {
                    record = new Questions_Table();
                    Global.isNull = true;
                } else
                {
                    Global.isNull = false;
                }
                record.Quiz_ID = model.Quiz_ID;
                record.QuestionNumber = model.QuestionNumber;
                record.QuestionText = model.QuestionText;
                record.FirstAnswer = model.FirstAnswer;
                record.SecondAnswer = model.SecondAnswer;
                record.ThirdAnswer = model.ThirdAnswer;
                record.FourthAnswer = model.FourthAnswer;
                record.IsFirstAnswerCorrect = model.IsFirstAnswerCorrect;
                record.IsSecondAnswerCorrect = model.IsSecondAnswerCorrect;
                record.IsThirdAnswerCorrect = model.IsThirdAnswerCorrect;
                record.isFourthAnswerCorrect = model.isFourthAnswerCorrect;
                record.Status = true;
                if (Global.isNull == true)
                {
                    db.Questions_Table.Add(record);
                }
                db.SaveChanges();
                return Ok(record.ID);
            }
            catch { return Ok(0); }
        }



        ///<summary>
        ///Edits a specified quiz.
        ///</summary>
        ///<param model = "model" >Uses the quizModel as a parameter.</param>
        ///<remarks>
        ///
        ///</remarks>
        ///<returns>
        ///Returns 1 if succeeded, 0 if failed.
        ///</returns> 
        [Route("api/quiz/editQuiz")]
        [HttpPost]
        public IHttpActionResult editQuiz(quizModel model)
        {
            try
            {
                var record = db.Quiz_Table.FirstOrDefault(d => d.ID == model.ID);

                if (record != null)
                {
                    record.QuizName = model.QuizName;
                    record.Description = model.Description;
                    record.QuizType = model.QuizType;
                    record.Image = model.Image;
                    record.Status = model.Status;
                    db.SaveChanges();
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch { return Ok(0); }
        }




        ///<summary>
        ///Deletes a specified quiz.
        ///</summary>
        ///<param model = "model" >Uses the quizModel as a parameter.</param>
        ///<remarks>
        ///
        ///</remarks>
        ///<returns>
        ///Returns 1 if succeeded, 0 if failed.
        ///</returns>       
        [Route("api/quiz/DeleteQuiz")]
        [HttpPost]
        public IHttpActionResult DeleteQuiz(quizModel model)
        {
            try
            {
                var record = db.Quiz_Table.FirstOrDefault(d => d.ID == model.ID);

                if (record != null)
                {
                    record.Status = false;
                    db.SaveChanges();
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch { return Ok(0); }
        }
    }
}

class Global
{
    public static bool isNull;

}