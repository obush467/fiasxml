namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stead")]
    public partial class Stead
    {
        [Column(TypeName = "text")]
        public string STEADGUID { get; set; }

        [StringLength(120)]
        public string NUMBER { get; set; }

        [Column(TypeName = "text")]
        public string REGIONCODE { get; set; }

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
        [Column(Order = 0)]
        public DateTime UPDATEDATE { get; set; }

        [Column(TypeName = "text")]
        public string PARENTGUID { get; set; }

        [Column(TypeName = "text")]
        public string STEADID { get; set; }

        [Column(TypeName = "text")]
        public string PREVID { get; set; }

        [Column(TypeName = "text")]
        public string NEXTID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OPERSTATUS { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime STARTDATE { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime ENDDATE { get; set; }

        [Column(TypeName = "text")]
        public string NORMDOC { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LIVESTATUS { get; set; }

        [StringLength(100)]
        public string CADNUM { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DIVTYPE { get; set; }
    }
}
