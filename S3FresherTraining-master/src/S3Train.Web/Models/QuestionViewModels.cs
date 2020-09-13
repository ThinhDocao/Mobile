using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public class QuestionViewModels
    {
        public QuestionViewModels()
        {

        }
        public QuestionViewModels(Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Content = question.Content;
            CreateDate = question.CreateDate;
            CreateBy = question.CreateBy;
            Status = question.Status;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public bool? Status { get; set; }
    }
}