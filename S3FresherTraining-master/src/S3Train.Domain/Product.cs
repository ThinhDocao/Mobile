using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class Product : EntityBase
    {
        public Guid ProductCategoryID { get; set; }
        public Guid BrandID { get; set; }
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

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public bool? IncludeVAT { get; set; }

        public int? Quantity { get; set; }


        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

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

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductSpecSpecValue> ProductSpecSpecValues { get; set; }


    }
}