using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class OrderDetail : EntityBase
    {
        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public Guid OrderID { get; set; }
        public Guid ProductID { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
