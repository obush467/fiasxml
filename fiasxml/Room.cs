namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        [Column(TypeName = "text")]
        public string ROOMGUID { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string FLATNUMBER { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FLATTYPE { get; set; }

        [StringLength(50)]
        public string ROOMNUMBER { get; set; }

        public int? ROOMTYPE { get; set; }

        [Column(TypeName = "text")]
        public string REGIONCODE { get; set; }

        [Column(TypeName = "text")]
        public string POSTALCODE { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime UPDATEDATE { get; set; }

        [Column(TypeName = "text")]
        public string HOUSEGUID { get; set; }

        [Column(TypeName = "text")]
        public string ROOMID { get; set; }

        [Column(TypeName = "text")]
        public string PREVID { get; set; }

        [Column(TypeName = "text")]
        public string NEXTID { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime STARTDATE { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime ENDDATE { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LIVESTATUS { get; set; }

        [Column(TypeName = "text")]
        public string NORMDOC { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OPERSTATUS { get; set; }

        [StringLength(100)]
        public string CADNUM { get; set; }

        [StringLength(100)]
        public string ROOMCADNUM { get; set; }
    }
}
