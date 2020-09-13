using S3Train.Contract;
using S3Train.Models;
using S3Train.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3Train.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductAdvertisementService _productAdvertisementService;
        private readonly IBrandService _brandService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IFooterClientService _footerClientService;
        private readonly IMenuService _menuService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly IMenuTypeService _menuTypeService;

        public ProductController(IProductService productService, IProductAdvertisementService productAdvertisementService, IBrandService brandService, IProductCategoryService productCategoryService, IFooterClientService footerClientService, IMenuService menuService, IContentCategoryService contentCategoryService, IMenuTypeService menuTypeService)
        {
            _productService = productService;
            _productAdvertisementService = productAdvertisementService;
            _productCategoryService = productCategoryService;
            _brandService = brandService;
            _footerClientService = footerClientService;
            _menuService = menuService;
            _contentCategoryService = contentCategoryService;
            _menuTypeService = menuTypeService;
        }
        // GET: ProductCategory
        public ActionResult ProductCategory(Guid id)
        {
            var view = _productService.ListAllByID(id);
            var model = view.Select(item => new ProductViewModel(item)).ToList();
            return View(model);
        }

        
    }
}