using Microsoft.AspNet.Identity;
using PagedList;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3Train.Web.Areas.Admin.Controllers
{
    public class AnswerQuestionController : Controller
    {
        // GET: Admin/AnswerQuestion
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;

        public AnswerQuestionController(IAnswerService answerService, IQuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }
        // GET: Admin/ProductCategory
        public ActionResult Index(string search, int? i)
        {
            if (search == null)
            {
                search = "";
            }
            else
            {
                i = 1;
            }
            ViewBag.answer = _answerService.ListAll();

            var question = _questionService.ListAll();

            var list = question.Select(item => new QuestionViewModels(item));

            var model = list.Where(x => x.Title.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10000);

            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(QuestionViewModels model)
        {
            var question = new Question()
            {
                Id = Guid.NewGuid(),
                Title=model.Title,
                Content=model.Content,
                CreateDate = model.CreateDate,
                CreateBy = User.Identity.GetUserName(),
                Status = true,
            };
            var result = _questionService.Create(question);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Detail(Guid id)
        {
            var question = _questionService.GetById(id);
            var answer = _answerService.ListQuestionID(question.Id);
            ViewBag.CountAnswer = answer.Count;
            ViewBag.answer = answer;
            ViewBag.question = question;
            return View();
        }

        [HttpPost]
        public ActionResult Detail(Guid id,AnswerViewModels model)
        {
            Answer ans = new Answer()
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                CreateDate = DateTime.Now,
                CreateBy = User.Identity.GetUserName(),
                QuestionID=id
            };

            var answerquestion = _answerService.Create(ans);
            return RedirectToAction("Detail");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _answerService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteList(Guid id)
        {
            _questionService.Delete(id);
            return RedirectToAction("Index");
        }
        



        [HttpPost]
        public JsonResult ChangeStatus(Guid id)
        {
            var result = _questionService.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}