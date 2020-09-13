using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class ContentCategory :EntityBase
    {

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

        public bool? Status { get; set; }

        public bool? ShowOnHome { get; set; }


        public virtual ICollection<Content> Contents { get; set; }
    }
}
