DECLARE @upserted TABLE (
	action nvarchar(50),
	old_AOID uniqueidentifier NULL,
	old_AOGUID uniqueidentifier NULL,
	old_FORMALNAME nvarchar(120) NULL,
	old_REGIONCODE nvarchar(2) NULL,
	old_AUTOCODE nvarchar(1) NULL,
	old_AREACODE nvarchar(3) NULL,
	old_CITYCODE nvarchar(3) NULL,
	old_CTARCODE nvarchar(3) NULL,
	old_PLACECODE nvarchar(3) NULL,
	old_STREETCODE nvarchar(4) NULL,
	old_EXTRCODE nvarchar(4) NULL,
	old_SEXTCODE nvarchar(3) NULL,
	old_OFFNAME nvarchar(120) NULL,
	old_POSTALCODE nvarchar(6) NULL,
	old_IFNSFL nvarchar(4) NULL,
	old_TERRIFNSFL nvarchar(4) NULL,
	old_IFNSUL nvarchar(4) NULL,
	old_TERRIFNSUL nvarchar(4) NULL,
	old_OKATO nvarchar(11) NULL,
	old_OKTMO nvarchar(11) NULL,
	old_UPDATEDATE date NULL,
	old_SHORTNAME nvarchar(10) NULL,
	old_AOLEVEL int NULL,
	old_PARENTGUID uniqueidentifier NULL,
	old_PREVID uniqueidentifier NULL,
	old_NEXTID uniqueidentifier NULL,
	old_CODE nvarchar(17) NULL,
	old_PLAINCODE nvarchar(15) NULL,
	old_ACTSTATUS int NULL,
	old_CENTSTATUS int NULL,
	old_OPERSTATUS int NULL,
	old_CURRSTATUS int NULL,
	old_STARTDATE date NULL,
	old_ENDDATE date NULL,
	old_NORMDOC uniqueidentifier NULL,
	old_LIVESTATUS bit NULL,
	old_CADNUM nvarchar(100) NULL,
	old_DIVTYPE int NULL,
	old_PLANCODE nvarchar(15) NULL,

	new_AOID uniqueidentifier NULL,
	new_AOGUID uniqueidentifier NULL,
	new_FORMALNAME nvarchar(120) NULL,
	new_REGIONCODE nvarchar(2) NULL,
	new_AUTOCODE nvarchar(1) NULL,
	new_AREACODE nvarchar(3) NULL,
	new_CITYCODE nvarchar(3) NULL,
	new_CTARCODE nvarchar(3) NULL,
	new_PLACECODE nvarchar(3) NULL,
	new_STREETCODE nvarchar(4) NULL,
	new_EXTRCODE nvarchar(4) NULL,
	new_SEXTCODE nvarchar(3) NULL,
	new_OFFNAME nvarchar(120) NULL,
	new_POSTALCODE nvarchar(6) NULL,
	new_IFNSFL nvarchar(4) NULL,
	new_TERRIFNSFL nvarchar(4) NULL,
	new_IFNSUL nvarchar(4) NULL,
	new_TERRIFNSUL nvarchar(4) NULL,
	new_OKATO nvarchar(11) NULL,
	new_OKTMO nvarchar(11) NULL,
	new_UPDATEDATE date NULL,
	new_SHORTNAME nvarchar(10) NULL,
	new_AOLEVEL int NULL,
	new_PARENTGUID uniqueidentifier NULL,
	new_PREVID uniqueidentifier NULL,
	new_NEXTID uniqueidentifier NULL,
	new_CODE nvarchar(17) NULL,
	new_PLAINCODE nvarchar(15) NULL,
	new_ACTSTATUS int NULL,
	new_CENTSTATUS int NULL,
	new_OPERSTATUS int NULL,
	new_CURRSTATUS int NULL,
	new_STARTDATE date NULL,
	new_ENDDATE date NULL,
	new_NORMDOC uniqueidentifier NULL,
	new_LIVESTATUS bit NULL,
	new_CADNUM nvarchar(100) NULL,
	new_DIVTYPE int NULL,
	new_PLANCODE nvarchar(15) NULL
)
WHILE (EXISTS (SELECT * FROM ##Object))
	BEGIN TRY
		BEGIN TRANSACTION
			MERGE fias.AddressObjects AS o
			  USING
				(select distinct * from ##Object) tmpo
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
			OUTPUT $action,deleted.*,inserted.* INTO @Upserted(	action,
				old_AOID,
				old_AOGUID,
				old_FORMALNAME,
				old_REGIONCODE,
				old_AUTOCODE,
				old_AREACODE,
				old_CITYCODE,
				old_CTARCODE,
				old_PLACECODE,
				old_STREETCODE,
				old_EXTRCODE,
				old_SEXTCODE,
				old_OFFNAME,
				old_POSTALCODE,
				old_IFNSFL,
				old_TERRIFNSFL,
				old_IFNSUL,
				old_TERRIFNSUL,
				old_OKATO,
				old_OKTMO,
				old_UPDATEDATE,
				old_SHORTNAME,
				old_AOLEVEL,
				old_PARENTGUID,
				old_PREVID,
				old_NEXTID,
				old_CODE,
				old_PLAINCODE,
				old_ACTSTATUS,
				old_CENTSTATUS,
				old_OPERSTATUS,
				old_CURRSTATUS,
				old_STARTDATE,
				old_ENDDATE,
				old_NORMDOC,
				old_LIVESTATUS,
				old_CADNUM,
				old_DIVTYPE,
				old_PLANCODE,
				new_AOID,
				new_AOGUID,
				new_FORMALNAME,
				new_REGIONCODE,
				new_AUTOCODE,
				new_AREACODE,
				new_CITYCODE,
				new_CTARCODE,
				new_PLACECODE,
				new_STREETCODE,
				new_EXTRCODE,
				new_SEXTCODE,
				new_OFFNAME,
				new_POSTALCODE,
				new_IFNSFL,
				new_TERRIFNSFL,
				new_IFNSUL,
				new_TERRIFNSUL,
				new_OKATO,
				new_OKTMO,
				new_UPDATEDATE,
				new_SHORTNAME,
				new_AOLEVEL,
				new_PARENTGUID,
				new_PREVID,
				new_NEXTID,
				new_CODE,
				new_PLAINCODE,
				new_ACTSTATUS,
				new_CENTSTATUS,
				new_OPERSTATUS,
				new_CURRSTATUS,
				new_STARTDATE,
				new_ENDDATE,
				new_NORMDOC,
				new_LIVESTATUS,
				new_CADNUM,
				new_DIVTYPE,
				new_PLANCODE );

			--вставка в AO_Names строк, где formalname  содержит одно слово
			INSERT INTO [address].[AO_Named]
					   ([AOID]
					   ,[OFFNAME_NAME]
					   ,[OFFNAME_NUM_NAME])
			select 
				ao.new_AOID,
				ao.new_OFFNAME,
				1 
			from @upserted ao
			left outer join  address.ao_named nao
			on ao.new_AOID=nao.AOID
			 where new_REGIONCODE in ('50','77')
			 and nao.AOID is null
			 and [dbo].[wordCount](new_FORMALNAME)=1

			delete from ##Object
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT N'Ошибка при обновлении ROOM'
		ROLLBACK TRANSACTION
	END CATCH