using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class OrderDetailsItemViewModels
    {
        public OrderDetailsItemViewModels() { }

        public Guid Id { get; set; }

        public Guid OrderID { get; set; }

        public Decimal? Price{ get; set; }
        public int? Quantity { get; set; }
        public string ShipName{ get; set; }
        public string ShipMobile { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? Status { get; set; }
    }
}