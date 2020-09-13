using S3Train.Domain;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace S3Train.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }
        public ProductViewModel(Product product)
        {
            Id = product.Id;
            ProductCategoryId = product.ProductCategoryID;
            Name = product.Name;
            Code = product.Code;
            MetaTitle = product.MetaTitle;
            Descriptions = product.Descriptions;
            Image = product.Image;
            MoreImage = product.MoreImage;
            Price = product.Price;
            PromotionPrice = product.PromotionPrice;
            IncludeVAT = product.IncludeVAT;
            Quantity = product.Quantity;
            Detail = product.Detail;
            Warranty = product.Warranty;
            CreateDate = product.CreateDate;
            CreateBy = product.CreateBy;
            ModifiedDate = product.ModifiedDate;
            ModifiedBy = product.ModifiedBy;
            Metakeywords = product.Metakeywords;
            MetaDescriptions = product.MetaDescriptions;
            Status = product.Status;
            TopHot = product.TopHot;
            ViewCount = product.ViewCount;
            Brand_Id = product.BrandID;


        }

        public Guid Id { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string Descriptions { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public decimal? Price { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public decimal? PromotionPrice { get; set; }

        public bool? IncludeVAT { get; set; }


        [RegularExpression("([1-9][0-9]*)")]
        public int? Quantity { get; set; }


        [Column(TypeName = "ntext")]
        [AllowHtml]
        public string Detail { get; set; }

        [RegularExpression("([1-9][0-9]*)")]
        public int? Warranty { get; set; }

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

        public bool? Status { get; set; }


        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
        public Guid Brand_Id { get; set; }

    }
}