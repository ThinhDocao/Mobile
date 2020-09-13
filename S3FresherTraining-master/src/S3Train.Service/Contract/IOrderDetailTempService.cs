using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IOrderDetailTempService : IGenenicServiceBase<OrderDetailTemp>
    {
        OrderDetailTemp ListAllByUserID(string UserID);
        void Create(OrderDetailTemp temp);
        void Update(OrderDetailTemp temp);
        void DeleteAll(Guid id);

    }
}
