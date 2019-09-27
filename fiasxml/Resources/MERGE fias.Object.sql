DECLARE @upserted TABLE (AOID uniqueidentifier)
MERGE fias.AddressObjects AS o
  USING
    (select distinct * from fias_tmp.[Object]) tmpo
  ON o.AOID=tmpo.AOID
  WHEN MATCHED AND 
    (
      dbo.eq(o.AOGUID,tmpo.AOGUID)=0
      OR dbo.eq(o.FORMALNAME,tmpo.FORMALNAME)=0
      OR dbo.eq(o.REGIONCODE,tmpo.REGIONCODE)=0
      OR dbo.eq(o.AUTOCODE,tmpo.AUTOCODE)=0
      OR dbo.eq(o.AREACODE,tmpo.AREACODE)=0
      OR dbo.eq(o.CITYCODE,tmpo.CITYCODE)=0
      OR dbo.eq(o.CTARCODE,tmpo.CTARCODE)=0
      OR dbo.eq(o.PLACECODE,tmpo.PLACECODE)=0
      OR dbo.eq(o.STREETCODE,tmpo.STREETCODE)=0
      OR dbo.eq(o.EXTRCODE,tmpo.EXTRCODE)=0
      OR dbo.eq(o.SEXTCODE,tmpo.SEXTCODE)=0
      OR dbo.eq(o.OFFNAME,tmpo.OFFNAME)=0
      OR dbo.eq(o.POSTALCODE,tmpo.POSTALCODE)=0
      OR dbo.eq(o.IFNSFL,tmpo.IFNSFL)=0
      OR dbo.eq(o.TERRIFNSFL,tmpo.TERRIFNSFL)=0
      OR dbo.eq(o.IFNSUL,tmpo.IFNSUL)=0
      OR dbo.eq(o.TERRIFNSUL,tmpo.TERRIFNSUL)=0
      OR dbo.eq(o.OKATO,tmpo.OKATO)=0
      OR dbo.eq(o.OKTMO,tmpo.OKTMO)=0
      OR dbo.eq(o.UPDATEDATE,tmpo.UPDATEDATE)=0
      OR dbo.eq(o.SHORTNAME,tmpo.SHORTNAME)=0
      OR dbo.eq(o.AOLEVEL,tmpo.AOLEVEL)=0
      OR dbo.eq(o.PARENTGUID,tmpo.PARENTGUID)=0
      OR dbo.eq(o.PREVID,tmpo.PREVID)=0
      OR dbo.eq(o.NEXTID,tmpo.NEXTID)=0
      OR dbo.eq(o.CODE,tmpo.CODE)=0
      OR dbo.eq(o.PLAINCODE,tmpo.PLAINCODE)=0
      OR dbo.eq(o.ACTSTATUS,tmpo.ACTSTATUS)=0
      OR dbo.eq(o.CENTSTATUS,tmpo.CENTSTATUS)=0
      OR dbo.eq(o.OPERSTATUS,tmpo.OPERSTATUS)=0
      OR dbo.eq(o.CURRSTATUS,tmpo.CURRSTATUS)=0
      OR dbo.eq(o.STARTDATE,tmpo.STARTDATE)=0
      OR dbo.eq(o.ENDDATE,tmpo.ENDDATE)=0
      OR dbo.eq(o.NORMDOC,tmpo.NORMDOC)=0
      OR dbo.eq(o.LIVESTATUS,tmpo.LIVESTATUS)=0
      OR dbo.eq(o.CADNUM,tmpo.CADNUM)=0
      OR dbo.eq(o.DIVTYPE,tmpo.DIVTYPE)=0
      OR dbo.eq(o.PLANCODE,tmpo.PLANCODE)=0
  )
  THEN UPDATE SET 
      AOGUID=tmpo.AOGUID
      ,FORMALNAME=tmpo.FORMALNAME
      ,REGIONCODE=tmpo.REGIONCODE
      ,AUTOCODE=tmpo.AUTOCODE
      ,AREACODE=tmpo.AREACODE
      ,CITYCODE=tmpo.CITYCODE
      ,CTARCODE=tmpo.CTARCODE
      ,PLACECODE=tmpo.PLACECODE
      ,STREETCODE=tmpo.STREETCODE
      ,EXTRCODE=tmpo.EXTRCODE
      ,SEXTCODE=tmpo.SEXTCODE
      ,OFFNAME=tmpo.OFFNAME
      ,POSTALCODE=tmpo.POSTALCODE
      ,IFNSFL=tmpo.IFNSFL
      ,TERRIFNSFL=tmpo.TERRIFNSFL
      ,IFNSUL=tmpo.IFNSUL
      ,TERRIFNSUL=tmpo.TERRIFNSUL
      ,OKATO=tmpo.OKATO
      ,OKTMO=tmpo.OKTMO
      ,UPDATEDATE=tmpo.UPDATEDATE
      ,SHORTNAME=tmpo.SHORTNAME
      ,AOLEVEL=tmpo.AOLEVEL
      ,PARENTGUID=tmpo.PARENTGUID
      ,PREVID=tmpo.PREVID
      ,NEXTID=tmpo.NEXTID
      ,CODE=tmpo.CODE
      ,PLAINCODE=tmpo.PLAINCODE
      ,ACTSTATUS=tmpo.ACTSTATUS
      ,CENTSTATUS=tmpo.CENTSTATUS
      ,OPERSTATUS=tmpo.OPERSTATUS
      ,CURRSTATUS=tmpo.CURRSTATUS
      ,STARTDATE=tmpo.STARTDATE
      ,ENDDATE=tmpo.ENDDATE
      ,NORMDOC=tmpo.NORMDOC
      ,LIVESTATUS=tmpo.LIVESTATUS
      ,CADNUM=tmpo.CADNUM
      ,DIVTYPE=tmpo.DIVTYPE
      ,PLANCODE=tmpo.PLANCODE
  WHEN NOT MATCHED --AND (tmpo.REGIONCODE='77' OR tmpo.REGIONCODE='50')
  THEN  INSERT (
            AOGUID
            ,FORMALNAME
            ,REGIONCODE
            ,AUTOCODE
            ,AREACODE
            ,CITYCODE
            ,CTARCODE
            ,PLACECODE
            ,STREETCODE
            ,EXTRCODE
            ,SEXTCODE
            ,OFFNAME
            ,POSTALCODE
            ,IFNSFL
            ,TERRIFNSFL
            ,IFNSUL
            ,TERRIFNSUL
            ,OKATO
            ,OKTMO
            ,UPDATEDATE
            ,SHORTNAME
            ,AOLEVEL
            ,PARENTGUID
            ,AOID
            ,PREVID
            ,NEXTID
            ,CODE
            ,PLAINCODE
            ,ACTSTATUS
            ,CENTSTATUS
            ,OPERSTATUS
            ,CURRSTATUS
            ,STARTDATE
            ,ENDDATE
            ,NORMDOC
            ,LIVESTATUS
            ,CADNUM
            ,DIVTYPE
            ,PLANCODE
)
        VALUES
          (
            tmpo.AOGUID
            ,tmpo.FORMALNAME
            ,tmpo.REGIONCODE
            ,tmpo.AUTOCODE
            ,tmpo.AREACODE
            ,tmpo.CITYCODE
            ,tmpo.CTARCODE
            ,tmpo.PLACECODE
            ,tmpo.STREETCODE
            ,tmpo.EXTRCODE
            ,tmpo.SEXTCODE
            ,tmpo.OFFNAME
            ,tmpo.POSTALCODE
            ,tmpo.IFNSFL
            ,tmpo.TERRIFNSFL
            ,tmpo.IFNSUL
            ,tmpo.TERRIFNSUL
            ,tmpo.OKATO
            ,tmpo.OKTMO
            ,tmpo.UPDATEDATE
            ,tmpo.SHORTNAME
            ,tmpo.AOLEVEL
            ,tmpo.PARENTGUID
            ,tmpo.AOID
            ,tmpo.PREVID
            ,tmpo.NEXTID
            ,tmpo.CODE
            ,tmpo.PLAINCODE
            ,tmpo.ACTSTATUS
            ,tmpo.CENTSTATUS
            ,tmpo.OPERSTATUS
            ,tmpo.CURRSTATUS
            ,tmpo.STARTDATE
            ,tmpo.ENDDATE
            ,tmpo.NORMDOC 
            ,tmpo.LIVESTATUS
            ,tmpo.CADNUM
            ,tmpo.DIVTYPE
            ,tmpo.PLANCODE
          )
OUTPUT inserted.AOID INTO @Upserted(AOID);
delete from fias_tmp.[Object] where 
	   AOID in (select AOID from @upserted)