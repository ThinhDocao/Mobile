using Microsoft.AspNet.Identity;
using PagedList;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Models;
using S3Train.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace S3Train.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IBrandService _brandService;

        public ProductController(IProductService productService, IProductCategoryService productAdvertisementService, IBrandService brandService)
        {
            _productService = productService;
            _productCategoryService = productAdvertisementService;
            _brandService = brandService;
        }

        
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

            ViewBag.productCategory = _productCategoryService.ListAll();

            ViewBag.brand = _brandService.ListAll();

            var product = _productService.ListAllOrderByCreateDate();

            var model = product.Select(item => new ProductViewModel(item));

            var a = model.Where(x => x.Name.Contains(search) || x.Price.ToString().Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10000);

            return View(a);
        }

        public ActionResult ProductInCategory(Guid id, string search, int? i)
        {
            if (search == null)
            {
                search = "";
            }
            else
            {
                i = 1;
            }
            ViewBag.Productcategory = _productCategoryService.ViewDetail(id);
            ViewBag.brand = _brandService.ListAll();
            var product = _productService.ListAllByID(id);
            var model = product.Select(item => new ProductViewModel(item));
            var a = model.Where(x => x.Name.Contains(search) || x.Price.ToString().Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 12);

            return View(a);
        }

        public ActionResult Create()
        {
            SetViewBagBrand();
            SetViewBagProductCategory();
            ViewBag.brand = _brandService.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                ProductCategoryID = model.ProductCategoryId,
                BrandID = model.Brand_Id,
                Code = model.Code,
                MetaTitle = model.MetaTitle,
                Descriptions = model.Descriptions,
                Image = model.Image,
                MoreImage = model.MoreImage,
                Price = model.Price,
                PromotionPrice = model.PromotionPrice,
                IncludeVAT = true,
                Quantity = model.Quantity,
                Detail = model.Detail,
                Warranty = model.Warranty,
                CreateDate = DateTime.Now,
                CreateBy = User.Identity.GetUserName(),
                ModifiedDate = DateTime.Now,
                ModifiedBy = model.ModifiedBy,
                Metakeywords = model.Metakeywords,
                MetaDescriptions = model.MetaDescriptions,
                Status = true,
                TopHot = model.TopHot,
                ViewCount = 0

            };
            var result = _productService.Create(product);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }

        public ActionResult Detail(Guid id, ProductViewModel model)
        {
            var product = _productService.GetById(id);
            model = new ProductViewModel(product);

            SetViewBagBrand(product.BrandID);
            SetViewBagProductCategory(product.ProductCategoryID);
            ViewBag.brand = _brandService.ListAll();
            return View(model);
        }

        public ActionResult Edit(Guid id, ProductViewModel model)
        {
            var product = _productService.GetById(id);
            model = new ProductViewModel(product);

            SetViewBagBrand(product.BrandID);
            SetViewBagProductCategory(product.ProductCategoryID);
            ViewBag.brand = _brandService.ListAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            var product = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                ProductCategoryID = model.ProductCategoryId,
                BrandID = model.Brand_Id,
                Code = model.Code,
                Descriptions = model.Descriptions,
                Image = model.Image,
                Price = model.Price,
                PromotionPrice = model.PromotionPrice,
                Quantity = model.Quantity,
                Detail = model.Detail,
                Warranty = model.Warranty,
                ModifiedDate = DateTime.Now,
                ModifiedBy = User.Identity.GetUserName(),
                MetaDescriptions = model.MetaDescriptions,
                TopHot = model.TopHot,
                ViewCount = 0
            };
            var result = _productService.Update(product);
            if (result == true)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }



        public void SetViewBagProductCategory(Guid? selectedId = null)
        {
            ViewBag.ProductCategoryID = new SelectList(_productCategoryService.ListAll(), "Id", "Name", selectedId);
        }
        public void SetViewBagBrand(Guid? selectedId = null)
        {
            ViewBag.BrandID = new SelectList(_brandService.ListAll(), "Id", "Name", selectedId);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: Admin/Product


        public JsonResult LoadImages(Guid id)
        {
            var product = _productService.GetById(id);
            var images = product.MoreImage;
            List<string> listImagesReturn = new List<string>();
            if (images == null)
            {
                return Json(new
                {
                    data = listImagesReturn
                }, JsonRequestBehavior.AllowGet);
            }
            XElement xImages = XElement.Parse(images);


            foreach (XElement element in xImages.Elements())
            {
                listImagesReturn.Add(element.Value);
            }
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveImages(Guid id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("Images");
            foreach (var item in listImages)
            {
                xElement.Add(new XElement("Image", item));
            }



            try
            {
                _productService.UpdateImages(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = false
                });
            }


        }


        [HttpPost]
        public JsonResult ChangeStatus(Guid id)
        {
            var result = _productService.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}