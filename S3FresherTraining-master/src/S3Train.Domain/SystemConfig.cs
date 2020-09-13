using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class SystemConfig:EntityBase
    {

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(250)]
        public string Value { get; set; }

        public bool? Status { get; set; }
    }
}
