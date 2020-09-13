using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class About :EntityBase
    {

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        public string Descriptions { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

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
    }
}
