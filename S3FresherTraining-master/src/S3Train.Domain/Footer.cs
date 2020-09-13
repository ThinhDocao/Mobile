using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class Footer:EntityBase
    {

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public bool? Status { get; set; }
    }
}
