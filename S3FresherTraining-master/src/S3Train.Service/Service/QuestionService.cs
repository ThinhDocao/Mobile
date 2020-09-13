using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class QuestionService : GenenicServiceBase<Question>, IQuestionService
    {
        public QuestionService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<Question> ListAll()
        {
            return this.EntityDbSet.Where(y => y.CreateDate <=DateTime.Now).ToList();
        }

        public bool Create(Question question)
        {
            this.DbContext.Questions.Add(question);
            this.DbContext.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var question = this.DbContext.Questions.Find(id);
            this.DbContext.Questions.Remove(question);
            this.DbContext.SaveChanges();
            return true;
        }

        public bool? ChangeStatus(Guid id)
        {
            var question = this.DbContext.Questions.Find(id);
            question.Status = !question.Status;
            this.DbContext.SaveChanges();
            return question.Status;

        }
    }
}
