using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class ProductCategoryViewModels
    {
        public ProductCategoryViewModels() { }
        public ProductCategoryViewModels(ProductCategory model)
        {
            Id = model.Id;
            Name = model.Name;
            MetaTitle = model.MetaTitle;
            ParentID = model.ParentID;
            DisplayOrder = model.DisplayOrder;
            SeoTitle = model.SeoTitle;
            CreateDate = model.CreateDate;
            CreateBy = model.CreateBy;
            ModifiedDate = model.ModifiedDate;
            ModifiedBy = model.ModifiedBy;
            Metakeywords = model.Metakeywords;
            MetaDescriptions = model.MetaDescriptions;
            Status = model.Status;
            ShowOnHome = model.ShowOnHome;
            Image = model.Image;
        }
        public Guid Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public long? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string Metakeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public bool? Status { get; set; }

        public bool? ShowOnHome { get; set; }
    }
}