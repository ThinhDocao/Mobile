using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class FooterClientService : GenenicServiceBase<Footer>, IFooterClientService
    {
        public FooterClientService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Footer ListAll()
        {
            return this.EntityDbSet.Where(y => y.Status == true).FirstOrDefault();
        }

        public bool Update(Footer footer)
        {
            var pro = this.DbContext.Footers.Find(footer.Id);
            pro.Content = footer.Content;
            this.DbContext.SaveChanges();
            return true;
        }
    }
}
