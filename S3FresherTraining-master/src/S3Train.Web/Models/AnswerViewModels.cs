using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class AnswerViewModels
    {
        public AnswerViewModels()
        {

        }
        public AnswerViewModels(Answer answer)
        {
            Id = answer.Id;
            Content = answer.Content;
            QuestionID = answer.QuestionID;
            CreateDate = answer.CreateDate;
            CreateBy = answer.CreateBy;
        }

        public Guid Id { get; set; }
        public Guid QuestionID { get; set; }
        public string Content { get; set; }
        public DateTime? CreateDate{ get; set; }
        public string CreateBy { get; set; }
    }
}