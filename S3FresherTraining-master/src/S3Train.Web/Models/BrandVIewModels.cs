using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class BrandVIewModels
    {
        public BrandVIewModels()
        {

        }
        public BrandVIewModels(Brand brand)
        {
            Id = brand.Id;
            Name = brand.Name;
            Logo = brand.Logo;
            CreateDate = brand.CreateDate;
            CreateBy = brand.CreateBy;
            ModifyDate = brand.ModifyDate;
            ModifyBy = brand.ModifyBy;
            Status = brand.Status;
            MetaTitle = brand.MetaTitle;
            DisplayOrder = brand.DisplayOrder;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public bool? Status { get; set; }
        public string MetaTitle { get; set; }

        public int? DisplayOrder { get; set; }
    }
}