using S3Train.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class AdminProductViewModels
    {
        public IList<ProductViewModel> Products { get; set; }
    }
}