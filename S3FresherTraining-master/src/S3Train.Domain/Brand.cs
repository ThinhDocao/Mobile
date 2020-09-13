using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Brand : EntityBase
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public bool? Status { get; set; }

        public string MetaTitle { get; set; }

        public int? DisplayOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
