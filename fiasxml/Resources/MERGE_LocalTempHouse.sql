WHILE (EXISTS (SELECT HOUSEID FROM #House))
	BEGIN TRY
	BEGIN
		BEGIN TRANSACTION
			DECLARE @upserted TABLE (
	[HOUSEID] [uniqueidentifier]  NOT NULL 
) 
MERGE  fias.House h
			USING (select distinct * from (select  top 10000 * from #House) iii)  th
			ON h.HOUSEID=th.HOUSEID
			WHEN MATCHED AND 
			  (
				dbo.eq(h.POSTALCODE,th.POSTALCODE)=0
				OR dbo.eq(h.IFNSFL,th.IFNSFL)=0
				OR dbo.eq(h.TERRIFNSFL,th.TERRIFNSFL)=0
				OR dbo.eq(h.IFNSUL,th.IFNSUL)=0
				OR dbo.eq(h.TERRIFNSUL,th.TERRIFNSUL)=0
				OR dbo.eq(h.OKATO,th.OKATO)=0
				OR dbo.eq(h.OKTMO,th.OKTMO)=0
				OR dbo.eq(h.UPDATEDATE,th.UPDATEDATE)=0
				OR dbo.eq(h.HOUSENUM,th.HOUSENUM)=0
				OR dbo.eq(h.ESTSTATUS,th.ESTSTATUS)=0
				OR dbo.eq(h.BUILDNUM,th.BUILDNUM)=0
				OR dbo.eq(h.STRUCNUM,th.STRUCNUM)=0
				OR dbo.eq(h.STRSTATUS,th.STRSTATUS)=0
				OR dbo.eq(h.HOUSEGUID,th.HOUSEGUID)=0
				OR dbo.eq(h.AOGUID,th.AOGUID)=0
				OR dbo.eq(h.STARTDATE,th.STARTDATE)=0
				OR dbo.eq(h.ENDDATE,th.ENDDATE)=0
				OR dbo.eq(h.STATSTATUS,th.STATSTATUS)=0
				OR dbo.eq(h.NORMDOC,th.NORMDOC)=0
				OR dbo.eq(h.COUNTER,th.COUNTER)=0
				OR dbo.eq(h.CADNUM,th.CADNUM)=0
				OR dbo.eq(h.DIVTYPE,th.DIVTYPE)=0
				)
			  THEN UPDATE SET
				POSTALCODE=th.POSTALCODE
				,IFNSFL=th.IFNSFL
				,TERRIFNSFL=th.TERRIFNSFL
				,IFNSUL=th.IFNSUL
				,TERRIFNSUL=th.TERRIFNSUL
				,OKATO=th.OKATO
				,OKTMO=th.OKTMO
				,UPDATEDATE=th.UPDATEDATE
				,HOUSENUM=th.HOUSENUM
				,ESTSTATUS=th.ESTSTATUS
				,BUILDNUM=th.BUILDNUM
				,STRUCNUM=th.STRUCNUM
				,STRSTATUS=th.STRSTATUS
				,HOUSEGUID=th.HOUSEGUID
				,AOGUID=th.AOGUID
				,STARTDATE=th.STARTDATE
				,ENDDATE=th.ENDDATE
				,STATSTATUS=th.STATSTATUS
				,NORMDOC=th.NORMDOC
				,COUNTER=th.COUNTER
				,CADNUM=th.CADNUM
				,DIVTYPE=th.DIVTYPE
			  WHEN NOT MATCHED 
			  THEN INSERT (  POSTALCODE
							,IFNSFL
							,TERRIFNSFL
							,IFNSUL
							,TERRIFNSUL
							,OKATO
							,OKTMO
							,UPDATEDATE
							,HOUSENUM
							,ESTSTATUS
							,BUILDNUM
							,STRUCNUM
							,STRSTATUS
							,HOUSEID
							,HOUSEGUID
							,AOGUID
							,STARTDATE
							,ENDDATE
							,STATSTATUS
							,NORMDOC
							,COUNTER
							,CADNUM
							,DIVTYPE)
				VALUES(
							 th.POSTALCODE
							,th.IFNSFL
							,th.TERRIFNSFL
							,th.IFNSUL
							,th.TERRIFNSUL
							,th.OKATO
							,th.OKTMO
							,th.UPDATEDATE
							,th.HOUSENUM
							,th.ESTSTATUS
							,th.BUILDNUM
							,th.STRUCNUM
							,th.STRSTATUS
							,th.HOUSEID
							,th.HOUSEGUID
							,th.AOGUID
							,th.STARTDATE
							,th.ENDDATE
							,th.STATSTATUS
							,th.NORMDOC
							,th.COUNTER
							,th.CADNUM
							,th.DIVTYPE)
			OUTPUT inserted.HOUSEID INTO @Upserted(HOUSEID);
			delete from #House		where HOUSEID in (select HOUSEID from @upserted)	
		COMMIT TRANSACTION
		END
	END TRY
	BEGIN CATCH
	PRINT N'Ошибка'
		ROLLBACK TRANSACTION
	END CATCH
