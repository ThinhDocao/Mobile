using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class OrderDetailTemp : EntityBase
    {
        public string UserID { get; set; }
        [Column(TypeName = "ntext")]
        public string CartContent { get; set; }

    }
}
