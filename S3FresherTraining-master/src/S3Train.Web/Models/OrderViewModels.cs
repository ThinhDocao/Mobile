using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class OrderViewModels
    {
        public OrderViewModels()
        {

        }
        public OrderViewModels(Order item)
        {
            Id = item.Id;
            CreateDate = item.CreateDate;
            CreateBy = item.CreateBy;
            ShipName = item.ShipName;
            ShipMobile = item.ShipMobile;
            ShipAddress = item.ShipAddress;
            ShipEmail = item.ShipEmail;
            Status = item.Status;
        }

        public Guid Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }
        public string ShipName { get; set; }

        public string ShipMobile { get; set; }

        public string ShipAddress { get; set; }

        public string ShipEmail { get; set; }

        public bool? Status { get; set; }

    }
}