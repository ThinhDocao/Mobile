using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Question : EntityBase
    {
        public string Title { get; set; }

        public string Content { get; set; }
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public bool? Status { get; set; }

        public virtual ICollection<Answer> Answers{ get; set; }
    }
}
