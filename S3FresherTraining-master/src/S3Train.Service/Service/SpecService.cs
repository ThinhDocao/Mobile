using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class SpecService : GenenicServiceBase<Spec>, ISpecService
    {
        public SpecService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public List<Spec> All()
        {
            return this.DbContext.Specs.Select(t => t).ToList();
        }
    }
}
