using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Order: EntityBase
    {
        public DateTime? CreateDate { get; set; }

        public string CreateBy { get; set; }
        [StringLength(50)]
        public string ShipName { get; set; }

        [StringLength(50)]
        public string ShipMobile { get; set; }

        [StringLength(50)]
        public string ShipAddress { get; set; }

        [StringLength(50)]
        public string ShipEmail { get; set; }

        public bool? Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
