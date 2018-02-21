namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IntervalStatus
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long INTVSTATID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(60)]
        public string NAME { get; set; }
    }
}
