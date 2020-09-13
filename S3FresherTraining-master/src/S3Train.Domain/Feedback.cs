using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class Feedback:EntityBase
    {

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(250)]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }
    }
}
