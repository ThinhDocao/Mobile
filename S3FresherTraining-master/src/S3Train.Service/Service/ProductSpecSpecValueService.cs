using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class ProductSpecSpecValueService : GenenicServiceBase<ProductSpecSpecValue>, IProductSpecSpecValueService
    {
        public ProductSpecSpecValueService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public List<ProductSpecSpecValue> loadTS(Guid ProductID)
        {
            return this.DbContext.productSpecSpecValues.Where(t => t.ProductID == ProductID).ToList();
        }
        public List<ProductSpecSpecValue> All()
        {
            return this.DbContext.productSpecSpecValues.Select(t => t).ToList();
        }
    }
}
