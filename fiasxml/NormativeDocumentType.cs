namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NormativeDocumentType")]
    public partial class NormativeDocumentType
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NDTYPEID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string NAME { get; set; }
    }
}
