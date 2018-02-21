namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EstateStatus
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ESTSTATID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string NAME { get; set; }

        [StringLength(20)]
        public string SHORTNAME { get; set; }
    }
}
