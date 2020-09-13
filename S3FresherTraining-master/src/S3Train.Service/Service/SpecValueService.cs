using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class SpecValueService : GenenicServiceBase<SpecValue>, ISpecValueService
    {
        public SpecValueService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public List<SpecValue> All()
        {
            return this.DbContext.SpecValues.Select(t => t).ToList();
        }
    }
}
