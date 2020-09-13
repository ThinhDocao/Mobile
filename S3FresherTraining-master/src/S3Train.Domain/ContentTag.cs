using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class ContentTag:EntityBase
    {
        public Guid ContentID { get; set; }
        public Guid TagID{ get; set; }

        public virtual Content Content{ get; set; }
        public virtual Tag Tag{ get; set; }
    }
}
