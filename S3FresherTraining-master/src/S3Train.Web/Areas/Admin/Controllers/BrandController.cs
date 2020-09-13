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
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IBrandService _brandService;

        public BrandController(IProductService productService, IProductCategoryService productCategoryService,IBrandService brandService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _brandService = brandService;
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

            ViewBag.product = _productService.ListAll();

            var brand = _brandService.ListAllOrderByCreateDate();

            var list = brand.Select(item => new BrandVIewModels(item));

            var model = list.Where(x => x.Name.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10000);

            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BrandVIewModels model)
        {
            var brand = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Logo = model.Logo,
                CreateDate = DateTime.Now,
                CreateBy = User.Identity.GetUserName(),
                ModifyDate = DateTime.Now,
                Status = true,
            };
            var result = _brandService.Create(brand);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Detail(Guid id, BrandVIewModels model)
        {
            var brand = _brandService.GetById(id);
            model = new BrandVIewModels(brand);
            return View(model);
        }

        public ActionResult Edit(Guid id, BrandVIewModels model)
        {
            var brand = _brandService.GetById(id);
            model = new BrandVIewModels(brand);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BrandVIewModels model)
        {
            var brand = new Brand()
            {
                Id = model.Id,
                Name = model.Name,
                Logo = model.Logo,
                ModifyDate = DateTime.Now,
                ModifyBy = User.Identity.GetUserName()
            };
            var result = _brandService.Update(brand);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _brandService.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult ChangeStatus(Guid id)
        {
            var result = _brandService.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}