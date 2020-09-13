using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class Tag:EntityBase
    {

        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<ContentTag> ContentTags { get; set; }
    }
}
