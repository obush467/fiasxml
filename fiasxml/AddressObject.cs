namespace fiasxml
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AddressObject")]
    public partial class AddressObject
    {
        public Guid? AOGUID { get; set; }

        [StringLength(120)]
        public string FORMALNAME { get; set; }

        public string REGIONCODE { get; set; }

        [StringLength(1)]
        public string AUTOCODE { get; set; }

        [StringLength(1000)]
        public string AREACODE { get; set; }

        [StringLength(1000)]
        public string CITYCODE { get; set; }

        [StringLength(1000)]
        public string CTARCODE { get; set; }

        [StringLength(1000)]
        public string PLACECODE { get; set; }

        [StringLength(1000)]
        public string STREETCODE { get; set; }

        [StringLength(1000)]
        public string EXTRCODE { get; set; }

        [StringLength(1000)]
        public string SEXTCODE { get; set; }

        [StringLength(1000)]
        public string OFFNAME { get; set; }

        [StringLength(6)]
        public string POSTALCODE { get; set; }

        [StringLength(1000)]
        public string IFNSFL { get; set; }

        [StringLength(1000)]
        public string TERRIFNSFL { get; set; }

        [StringLength(1000)]
        public string IFNSUL { get; set; }

        [StringLength(1000)]
        public string TERRIFNSUL { get; set; }

        [StringLength(1000)]
        public string OKATO { get; set; }

        [StringLength(11)]
        public string OKTMO { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        [StringLength(10)]
        public string SHORTNAME { get; set; }

        public long? AOLEVEL { get; set; }

        public Guid? PARENTGUID { get; set; }

        [Key]
        public Guid AOID { get; set; }

        public Guid? PREVID { get; set; }

        public Guid? NEXTID { get; set; }

        [StringLength(17)]
        public string CODE { get; set; }

        [StringLength(15)]
        public string PLAINCODE { get; set; }

        public long? ACTSTATUS { get; set; }

        public long? CENTSTATUS { get; set; }

        public long? OPERSTATUS { get; set; }

        public long? CURRSTATUS { get; set; }

        public DateTime? STARTDATE { get; set; }

        public DateTime? ENDDATE { get; set; }

        public Guid? NORMDOC { get; set; }

        [StringLength(1000)]
        public string LIVESTATUS { get; set; }

        [StringLength(1000)]
        public string CADNUM { get; set; }

        [StringLength(1000)]
        public string DIVTYPE { get; set; }
    }
}
