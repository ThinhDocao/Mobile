using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class Content:EntityBase
    {
        public Guid ContentCategoryID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string Descriptions { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

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

        [StringLength(500)]
        public string Tags { get; set; }

        public virtual ContentCategory ContentCategory { get; set; }
        public virtual ICollection<ContentTag> ContentTags { get; set; }
    }
}
