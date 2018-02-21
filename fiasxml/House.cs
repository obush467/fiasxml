namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("House")]
    public partial class House
    {
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

        [StringLength(20)]
        public string HOUSENUM { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ESTSTATUS { get; set; }

        [StringLength(10)]
        public string BUILDNUM { get; set; }

        [StringLength(10)]
        public string STRUCNUM { get; set; }

        public long? STRSTATUS { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public System.Guid HOUSEID { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public string HOUSEGUID { get; set; }

        [Column(TypeName = "text")]
        public string AOGUID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime STARTDATE { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime ENDDATE { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long STATSTATUS { get; set; }

        [Column(TypeName = "text")]
        public string NORMDOC { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long COUNTER { get; set; }

        [StringLength(100)]
        public string CADNUM { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DIVTYPE { get; set; }
    }
}
