using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class MenuTypeService : GenenicServiceBase<MenuType>, IMenuTypeService
    {
        public MenuTypeService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<MenuType> ListAll()
        {
            return this.EntityDbSet.ToList();
        }
    }
}
