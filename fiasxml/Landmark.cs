namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Landmark")]
    public partial class Landmark
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(500)]
        public string LOCATION { get; set; }

        [Column(TypeName = "text")]
        public string POSTALCODE { get; set; }

        [Column(TypeName = "text")]
        public string IFNSFL { get; set; }

        [Column(TypeName = "text")]
        public string TERRIFNSFL { get; set; }

        [Column(TypeName = "text")]
        public string IFNSUL { get; set; }

        [Column(TypeName = "text")]
        public string TERRIFNSUL { get; set; }

        [Column(TypeName = "text")]
        public string OKATO { get; set; }

        [StringLength(11)]
        public string OKTMO { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime UPDATEDATE { get; set; }

        [Column(TypeName = "text")]
        public string LANDID { get; set; }

        [Column(TypeName = "text")]
        public string LANDGUID { get; set; }

        [Column(TypeName = "text")]
        public string AOGUID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime STARTDATE { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime ENDDATE { get; set; }

        [Column(TypeName = "text")]
        public string NORMDOC { get; set; }
    }
}
