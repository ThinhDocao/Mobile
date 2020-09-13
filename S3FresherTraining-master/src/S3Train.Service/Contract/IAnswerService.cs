using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Contract
{
    public interface IAnswerService : IGenenicServiceBase<Answer>
    {
        List<Answer> ListAll();
        List<Answer> ListQuestionID(Guid id);

        bool Create(Answer answer);
        bool Delete(Guid id);
    }
}
