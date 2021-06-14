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
            var result = db.Quiz_Table.ToList();
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
                record.Status = model.Status;

                db.Quiz_Table.Add(record);
                db.SaveChanges();
                return Ok(1);
            }
            catch { return Ok(0); }
        }
    }
}
