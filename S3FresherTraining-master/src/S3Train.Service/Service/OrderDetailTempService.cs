using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class OrderDetailTempService : GenenicServiceBase<OrderDetailTemp>, IOrderDetailTempService
    {
        public OrderDetailTempService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public OrderDetailTemp ListAllByUserID(string UserID)
        {
            return this.EntityDbSet.FirstOrDefault(x => x.UserID == UserID);
        }

        public void Create(OrderDetailTemp temp)
        {
            this.DbContext.OrderDetailTemps.Add(temp);
            this.DbContext.SaveChanges();
        }

        public void Update(OrderDetailTemp temp)
        {
            var save = this.DbContext.OrderDetailTemps.Find(temp.Id);
            save.UserID = temp.UserID;
            save.CartContent = temp.CartContent;
            this.DbContext.SaveChanges();
        }

        public void DeleteAll(Guid id)
        {
            var save = this.DbContext.OrderDetailTemps.Find(id);
            this.DbContext.OrderDetailTemps.Remove(save);
            this.DbContext.SaveChanges();
        }
    }
}
