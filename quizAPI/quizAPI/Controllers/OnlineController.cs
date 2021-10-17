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





        [Route("api/shopping/getShoppingLists")]
        [HttpGet]
        public IHttpActionResult getShoppingLists()
        {
            var result = db.dbo_Shopping_Lists.Where(d => d.Status == true).ToList();
            return Ok(result);
        }


        [Route("api/shopping/addList")]
        [HttpPost]
        public IHttpActionResult addList(listModel model)
        {
            try
            {
                var existingList = db.dbo_Shopping_Lists.FirstOrDefault(d => d.listName == model.listName & d.Status == true);

                if (existingList == null)
                {
                    var record = new dbo_Shopping_Lists();
                    record.listName = model.listName;
                    record.Status = true;

                    db.dbo_Shopping_Lists.Add(record);
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



        [Route("api/shopping/editListName")]
        [HttpPost]
        public IHttpActionResult editQuiz(listModel model)
        {
            try
            {
                var record = db.dbo_Shopping_Lists.FirstOrDefault(d => d.ID == model.ID);

                if (record != null)
                {              
                    record.listName = model.listName;
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


        [Route("api/shopping/deleteList")]
        [HttpGet]
        public IHttpActionResult deleteList(int ID)
        {
            try
            {
                var result = db.dbo_Shopping_Lists.FirstOrDefault(d => d.ID == ID);
                db.dbo_Shopping_Lists.Remove(result);
                db.SaveChanges();
                return Ok(1);
            } 
            catch
            {
                return Ok(0);
            }
        }















        [Route("api/quiz/getQuizzes")]
        [HttpGet]
        public IHttpActionResult getQuizzes()
        {
            var result = db.Quiz_Table.Where(d => d.Status == true).ToList();
            return Ok(result);
        }



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



        [Route("api/quiz/getQuiz")]
        [HttpGet]
        public IHttpActionResult getQuiz(int ID)
        {
            var result = db.Quiz_Table.Where(d => d.ID == ID).ToList();
            return Ok(result);
        }



        [Route("api/quiz/getQuestionData")]
        [HttpGet]
        public IHttpActionResult getQuestionData(int QuizId, int questionNumber)
        {
            var result = db.Questions_Table.Where(d => d.Quiz_ID == QuizId && d.QuestionNumber == questionNumber).ToList();
            return Ok(result);
        }



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



        [Route("api/quiz/getAccountDetails")]
        [HttpGet]
        public IHttpActionResult getAccountDetails(string Username, string Password)
        {
            var result = db.Account_Table.Where(d => d.Username == Username && d.Password == Password).ToList();
            return Ok(result);
        }




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