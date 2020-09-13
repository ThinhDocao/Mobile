using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Answer : EntityBase
    {
        public Guid QuestionID { get; set; }
        public string  Content { get; set; }
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public virtual Question Question{ get; set; }
    }
}
