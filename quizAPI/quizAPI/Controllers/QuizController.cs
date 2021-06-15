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

        [Route("api/quiz/getQuizzes")]
        [HttpGet]
        public IHttpActionResult getQuizzes()
        {
            var result = db.Quiz_Table.Where(d => d.Status == true).ToList();
            return Ok(result);
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

        //[Authorize]
        [Route("api/quiz/createQuiz")]
        [HttpPost]
        public IHttpActionResult createQuiz(quizModel model)
        {
            try
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
            }
            catch { return Ok(0); }
        }

        [Route("api/quiz/createQuestionAndAnswers")]
        [HttpPost]
        public IHttpActionResult createQuestionAndAnswers(questionModel model)
        {
            try
            {
                var record = new Questions_Table();
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

                db.Questions_Table.Add(record);
                db.SaveChanges();
                return Ok(record.ID);
            }
            catch { return Ok(0); }
        }

        //[Authorize]
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

        //[Authorize]
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
