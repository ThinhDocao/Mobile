using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class ContentCategoryService : GenenicServiceBase<ContentCategory>, IContentCategoryService
    {
        public ContentCategoryService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<ContentCategory> ListAllDisplayOrder()
        {
            return this.EntityDbSet.Where(x=>x.Status==true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
