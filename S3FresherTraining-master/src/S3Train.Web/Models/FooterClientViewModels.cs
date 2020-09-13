using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3Train.Web.Models
{
    public class FooterClientViewModels
    {
        public FooterClientViewModels()
        {

        }
        public FooterClientViewModels(Footer item)
        {
            Id = item.Id;
            Content = item.Content;
            Status = item.Status;
        }

        public Guid Id { get; set; }



        [Column(TypeName = "ntext")]
        [AllowHtml]
        public string Content { get; set; }

        public bool? Status { get; set; }
    }
}