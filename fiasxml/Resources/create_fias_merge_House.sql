CREATE PROCEDURE [fias].[merge_House]	
(
	@POSTALCODE varchar(6) NULL,
	@IFNSFL varchar(4) NULL,
	@TERRIFNSFL varchar(4) NULL,
	@IFNSUL varchar(4) NULL,
	@TERRIFNSUL varchar(4) NULL,
	@OKATO varchar(11) NULL,
	@OKTMO varchar(11) NULL,
	@UPDATEDATE smalldatetime NULL,
	@HOUSENUM varchar(20) NULL,
	@ESTSTATUS int NULL,
	@BUILDNUM varchar(10) NULL,
	@STRUCNUM varchar(10) NULL,
	@STRSTATUS int NULL,
	@HOUSEID varchar(36) NULL,
	@HOUSEGUID varchar(36) NULL,
	@AOGUID varchar(36) NULL,
	@STARTDATE smalldatetime NULL,
	@ENDDATE smalldatetime NULL,
	@STATSTATUS int NULL,
	@NORMDOC varchar(36) NULL,
	@COUNTER int NULL,
	@CADNUM varchar(100) NULL,
	@DIVTYPE int NULL)
AS	
	MERGE  fias.House h
			USING (			
			select 
			@POSTALCODE POSTALCODE
							,@IFNSFL IFNSFL
							,@TERRIFNSFL TERRIFNSFL
							,@IFNSUL IFNSUL
							,@TERRIFNSUL TERRIFNSUL
							,@OKATO OKATO
							,@OKTMO OKTMO
							,@UPDATEDATE UPDATEDATE
							,@HOUSENUM   HOUSENUM
							,@ESTSTATUS  ESTSTATUS
							,@BUILDNUM  BUILDNUM
							,@STRUCNUM  STRUCNUM
							,@STRSTATUS  STRSTATUS
							,@HOUSEID  HOUSEID
							,@HOUSEGUID  HOUSEGUID
							,@AOGUID  AOGUID
							,@STARTDATE  STARTDATE
							,@ENDDATE  ENDDATE
							,@STATSTATUS  STATSTATUS
							,@NORMDOC  NORMDOC
							,@COUNTER  COUNTER
							,@CADNUM  CADNUM  
							,@DIVTYPE  DIVTYPE
			) th
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
				OUTPUT $action,inserted.*,deleted.*;