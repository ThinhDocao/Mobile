using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class OrderDetailService : GenenicServiceBase<OrderDetail>, IOrderDetailService
    {
        public OrderDetailService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<OrderDetail> ListAll()
        {
            return this.EntityDbSet.ToList();
        }

        public List<OrderDetail> ListAllByIdOrder(Guid id)
        {
            return this.DbContext.OrderDetails.Where(x => x.OrderID == id).ToList();
        }

        public bool Delete(Guid id)
        {
            var orderDetail = this.DbContext.OrderDetails.Find(id);
            this.DbContext.OrderDetails.Remove(orderDetail);
            this.DbContext.SaveChanges();
            return true;
        }
        public bool Insert(OrderDetail orderDetail)
        {
            this.DbContext.OrderDetails.Add(orderDetail);
            this.DbContext.SaveChanges();
            return true;
        }
    }
}
