using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class ProductSpecSpecValue : EntityBase
    {
        public Guid ProductID { get; set; }
        public Guid SpecID { get; set; }
        public Guid SpecValueID { get; set; }
        public virtual Product Product { get; set; }

        public virtual Spec Spec { get; set; }

        public virtual SpecValue SpecValue { get; set; }
    }
}
