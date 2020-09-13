using PagedList;
using S3Train.Contract;
using S3Train.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3Train.Web.Areas.Admin.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: Admin/OrderDetail

        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;

        public OrderDetailController(IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _productService = productService;
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

            var orderDetail = _orderDetailService.ListAll();

            var order = _orderService.ListAll();

            var product = _productService.ListAll();


            var model = from a in orderDetail
                        join b in order
                        on a.OrderID equals b.Id
                        join c in product
                        on a.ProductID equals c.Id
                        group a by new { OrderDetailID=a.OrderID,ShipName=b.ShipName,CreateDate=b.CreateDate,ShipMobile=b.ShipMobile,b.Status } into ag
                        select new
                        {
                            OrderDetailID=ag.Key.OrderDetailID,
                            ShipName=ag.Key.ShipName,
                            CreateDate=ag.Key.CreateDate,
                            ShipMobile=ag.Key.ShipMobile,
                            Quantity = ag.Sum(x=>x.Quantity),
                            Price=ag.Sum(x=>x.Price),
                            Status=ag.Key.Status
                        };

            var g = model.Select(item => new OrderDetailsItemViewModels()
            {
                Id = item.OrderDetailID,
                ShipName=item.ShipName,
                CreateDate=item.CreateDate,
                ShipMobile=item.ShipMobile,
                Quantity=item.Quantity,
                Price=item.Price,
                Status=item.Status
            });
            var list = g.Where(x => x.ShipName.Contains(search)|| x.Name.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 10000);




            return View(list);
        }

        public ActionResult Detail(Guid id, OrderDetailsItemViewModels model)
        {
            var orderDetail = _orderDetailService.ListAll();

            var order = _orderService.ListAll();

            var product = _productService.ListAll();

            var joi = from a in orderDetail
                        join b in order
                        on a.OrderID equals b.Id
                        join c in product
                        on a.ProductID equals c.Id
                      where a.Id==id
                        select new
                        {
                            a.Id,
                            a.Price,
                            a.Quantity,
                            b.ShipName,
                            b.ShipMobile,
                            b.ShipAddress,
                            b.ShipEmail,
                            b.CreateDate,
                            b.CreateBy,
                            b.Status,
                            c.Name,
                            c.Code,
                        };
            var ViewID = joi.Select(item => new OrderDetailsItemViewModels()
            {
                Id = id,
                Price = item.Price,
                Quantity = item.Quantity,
                ShipName = item.ShipName,
                ShipAddress = item.ShipAddress,
                ShipMobile = item.ShipMobile,
                ShipEmail = item.ShipEmail,
                Code = item.Code,
                Name = item.Name,
                Status = item.Status,
                CreateBy = item.CreateBy,
                CreateDate = item.CreateDate
            }).FirstOrDefault();


            ViewBag.OrderDetailByOrder = _orderDetailService.ListAllByIdOrder(id);
            ViewBag.Order = _orderService.GetById(id);

            List<string> list = new List<string>();
            List<decimal?> list2 = new List<decimal?>();
            foreach (var item in _orderDetailService.ListAllByIdOrder(id))
            {
                var a = _productService.GetById(item.ProductID) ;
                list.Add(a.Name);
                list2.Add(a.Price);
            }
            ViewBag.ListName = list;
            ViewBag.ListPrice = list2;
            return View(ViewID);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _orderDetailService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}