using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class AnswerService : GenenicServiceBase<Answer>, IAnswerService
    {
        public AnswerService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<Answer> ListAll()
        {
            return this.EntityDbSet.ToList();
        }
        public List<Answer> ListQuestionID(Guid id)
        {
            return this.EntityDbSet.Where(x=>x.QuestionID==id).OrderByDescending(x=>x.CreateDate).ToList();
        }

        public bool Create(Answer answer)
        {
            this.DbContext.answers.Add(answer);
            this.DbContext.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var answer = this.DbContext.answers.Find(id);
            this.DbContext.answers.Remove(answer);
            this.DbContext.SaveChanges();
            return true;
        }
    }
}
