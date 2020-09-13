using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class OrderService : GenenicServiceBase<Order>, IOrderService
    {
        public OrderService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public List<Order> ListAll()
        {
            return this.EntityDbSet.ToList();
        }
        public Guid Insert(Order order)
        {
            this.DbContext.Orders.Add(order);
            this.DbContext.SaveChanges();
            return order.Id;
        }

    }
}
