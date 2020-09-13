using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class OrderDetailViewModels
    {
        public OrderDetailViewModels()
        {

        }
        public OrderDetailViewModels(OrderDetail item)
        {
            Id = item.Id;
            Quantity = item.Quantity;
            Price = item.Price;
            OrderID = item.OrderID;
            ProductID = item.ProductID;
        }

        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }

    }
}