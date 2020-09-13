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
    public class FooterClientController : Controller
    {
        // GET: Admin/FooterClient
        private readonly IFooterClientService _footerClientService;
        public FooterClientController(IFooterClientService footerClientService)
        {
            _footerClientService = footerClientService;
        }
        public ActionResult Index()
        {

            var footer = _footerClientService.ListAll();

            FooterClientViewModels model = new FooterClientViewModels(footer);


            return View(model);
        }
        [HttpPost]
        public ActionResult Index(FooterClientViewModels model)
        {

            var footer = new Footer()
            {
                Id = model.Id,
                Content = model.Content,
            };
            var result = _footerClientService.Update(footer);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }
    }
}