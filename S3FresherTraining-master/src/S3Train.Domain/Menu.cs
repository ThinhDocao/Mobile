using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class Menu:EntityBase
    {

        public Guid MenuTypeID { get; set; }
        [StringLength(50)]
        public string Text { get; set; }
        public string Image { get; set; }


        [StringLength(250)]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        public bool? Status { get; set; }

        public virtual MenuType MenuType { get; set; }

    }
}
