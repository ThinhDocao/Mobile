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
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductService productService, IProductCategoryService productAdvertisementService)
        {
            _productService = productService;
            _productCategoryService = productAdvertisementService;
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

            var productCategory = _productCategoryService.ListAllOrderByCreateDate();

            var list = productCategory.Select(item => new ProductCategoryViewModels(item));

            var model = list.Where(x => x.Name.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10000);

            return View(model);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategoryViewModels model)
        {
            var productCategory = new ProductCategory()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Image = model.Image,
                CreateDate = DateTime.Now,
                CreateBy = User.Identity.GetUserName(),
                ModifiedDate = DateTime.Now,
                Status = true,
            };
            var result = _productCategoryService.Create(productCategory);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Detail(Guid id, ProductCategoryViewModels model)
        {
            var productCategory = _productCategoryService.GetById(id);
            model = new ProductCategoryViewModels(productCategory);
            return View(model);
        }

        public ActionResult Edit(Guid id, ProductCategoryViewModels model)
        {
            var productCategory = _productCategoryService.GetById(id);
            model = new ProductCategoryViewModels(productCategory);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategoryViewModels model)
        {
            var productCategory = new ProductCategory()
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                ModifiedDate = DateTime.Now,
                ModifiedBy = User.Identity.GetUserName()
            };
            var result = _productCategoryService.Update(productCategory);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }


        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _productCategoryService.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult ChangeStatus(Guid id)
        {
            var result = _productCategoryService.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}