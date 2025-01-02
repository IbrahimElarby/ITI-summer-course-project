namespace tryno2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Take")]
    public partial class Take
    {
        public int St_id { get; set; }

        public int Course_id { get; set; }

        public int id { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }
}
