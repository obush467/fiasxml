namespace fiasxml
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Models.ActualStatus> ActualStatus { get; set; }
        public virtual DbSet<AddressObject> AddressObject { get; set; }
        public virtual DbSet<AddressObjectType> AddressObjectType { get; set; }
        public virtual DbSet<CenterStatus> CenterStatus { get; set; }
        public virtual DbSet<CurrentStatus> CurrentStatus { get; set; }
        public virtual DbSet<EstateStatus> EstateStatus { get; set; }
        public virtual DbSet<House> House { get; set; }
        public virtual DbSet<HouseInterval> HouseInterval { get; set; }
        public virtual DbSet<HouseStateStatus> HouseStateStatus { get; set; }
        public virtual DbSet<IntervalStatus> IntervalStatus { get; set; }
        public virtual DbSet<Landmark> Landmark { get; set; }
        public virtual DbSet<NormativeDocument> NormativeDocument { get; set; }
        public virtual DbSet<NormativeDocumentType> NormativeDocumentType { get; set; }
        public virtual DbSet<OperationStatus> OperationStatus { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Stead> Stead { get; set; }
        public virtual DbSet<StructureStatus> StructureStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ActualStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.FORMALNAME)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.REGIONCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.AUTOCODE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.AREACODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.CITYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.CTARCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.PLACECODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.STREETCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.EXTRCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.SEXTCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.OFFNAME)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.POSTALCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.IFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.TERRIFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.IFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.TERRIFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.OKATO)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.OKTMO)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.SHORTNAME)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.CODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.PLAINCODE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.LIVESTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.CADNUM)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObject>()
                .Property(e => e.DIVTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObjectType>()
                .Property(e => e.SCNAME)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObjectType>()
                .Property(e => e.SOCRNAME)
                .IsUnicode(false);

            modelBuilder.Entity<AddressObjectType>()
                .Property(e => e.KOD_T_ST)
                .IsUnicode(false);

            modelBuilder.Entity<CenterStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<CurrentStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<EstateStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<EstateStatus>()
                .Property(e => e.SHORTNAME)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.POSTALCODE)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.IFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.TERRIFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.IFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.TERRIFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.OKATO)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.OKTMO)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.HOUSENUM)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.BUILDNUM)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.STRUCNUM)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.HOUSEID)
                ;

            modelBuilder.Entity<House>()
                .Property(e => e.HOUSEGUID)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.AOGUID)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.NORMDOC)
                .IsUnicode(false);

            modelBuilder.Entity<House>()
                .Property(e => e.CADNUM)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.POSTALCODE)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.IFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.TERRIFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.IFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.TERRIFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.OKATO)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.OKTMO)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.HOUSEINTID)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.INTGUID)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.AOGUID)
                .IsUnicode(false);

            modelBuilder.Entity<HouseInterval>()
                .Property(e => e.NORMDOC)
                .IsUnicode(false);

            modelBuilder.Entity<HouseStateStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<IntervalStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.LOCATION)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.POSTALCODE)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.IFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.TERRIFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.IFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.TERRIFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.OKATO)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.OKTMO)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.LANDID)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.LANDGUID)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.AOGUID)
                .IsUnicode(false);

            modelBuilder.Entity<Landmark>()
                .Property(e => e.NORMDOC)
                .IsUnicode(false);

            modelBuilder.Entity<NormativeDocument>()
                .Property(e => e.NORMDOCID)
                .IsUnicode(false);

            modelBuilder.Entity<NormativeDocument>()
                .Property(e => e.DOCNAME)
                .IsUnicode(false);

            modelBuilder.Entity<NormativeDocument>()
                .Property(e => e.DOCNUM)
                .IsUnicode(false);

            modelBuilder.Entity<NormativeDocumentType>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<OperationStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.ROOMGUID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.FLATNUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.ROOMNUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.REGIONCODE)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.POSTALCODE)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.HOUSEGUID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.ROOMID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.PREVID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.NEXTID)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.NORMDOC)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.CADNUM)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.ROOMCADNUM)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.STEADGUID)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.REGIONCODE)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.POSTALCODE)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.IFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.TERRIFNSFL)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.IFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.TERRIFNSUL)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.OKATO)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.OKTMO)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.PARENTGUID)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.STEADID)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.PREVID)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.NEXTID)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.NORMDOC)
                .IsUnicode(false);

            modelBuilder.Entity<Stead>()
                .Property(e => e.CADNUM)
                .IsUnicode(false);

            modelBuilder.Entity<StructureStatus>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<StructureStatus>()
                .Property(e => e.SHORTNAME)
                .IsUnicode(false);
        }
    }
}
