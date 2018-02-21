namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NormativeDocument")]
    public partial class NormativeDocument
    {
        [Column(TypeName = "text")]
        public string NORMDOCID { get; set; }

        [Column(TypeName = "text")]
        public string DOCNAME { get; set; }

        public DateTime? DOCDATE { get; set; }

        [StringLength(20)]
        public string DOCNUM { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DOCTYPE { get; set; }

        public long? DOCIMGID { get; set; }
    }
}
