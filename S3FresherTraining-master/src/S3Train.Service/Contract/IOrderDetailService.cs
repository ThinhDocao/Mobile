using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IOrderDetailService : IGenenicServiceBase<OrderDetail>
    {
        List<OrderDetail> ListAll();
        bool Delete(Guid id);
        bool Insert(OrderDetail orderDetail);
        List<OrderDetail> ListAllByIdOrder(Guid id);
    }
}
