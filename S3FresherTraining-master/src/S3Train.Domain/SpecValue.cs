using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class SpecValue : EntityBase
    {
        [StringLength(255)]
        public string NameSpecValue { get; set; }
        public virtual ICollection<ProductSpecSpecValue> ProductSpecSpecVaLues { get; set; }
    }
}
