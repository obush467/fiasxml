namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HouseInterval")]
    public partial class HouseInterval
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

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long INTSTART { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long INTEND { get; set; }

        [Column(TypeName = "text")]
        public string HOUSEINTID { get; set; }

        [Column(TypeName = "text")]
        public string INTGUID { get; set; }

        [Column(TypeName = "text")]
        public string AOGUID { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime STARTDATE { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime ENDDATE { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long INTSTATUS { get; set; }

        [Column(TypeName = "text")]
        public string NORMDOC { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long COUNTER { get; set; }
    }
}
