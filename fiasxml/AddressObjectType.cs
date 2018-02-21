namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AddressObjectType")]
    public partial class AddressObjectType
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long LEVEL { get; set; }

        [StringLength(10)]
        public string SCNAME { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string SOCRNAME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string KOD_T_ST { get; set; }
    }
}
