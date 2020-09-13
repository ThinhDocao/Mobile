using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IBrandService : IGenenicServiceBase<Brand>
    {
        List<Brand> ListAll();
        bool? ChangeStatus(Guid id);
        List<Brand> ListAllOrderByCreateDate();
        bool Create(Brand brand);
        bool Update(Brand brand);
        bool Delete(Guid id);
    }
}
