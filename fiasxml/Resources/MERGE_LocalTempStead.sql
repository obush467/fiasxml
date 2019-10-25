WHILE (EXISTS (SELECT * FROM #Stead))
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @upserted TABLE (ID uniqueidentifier)
			MERGE fias.Stead room
				USING (SELECT DISTINCT * FROM (SELECT TOP 100000 * FROM #Stead) TTT) temproom
				ON room.STEADID=temproom.STEADID
			WHEN MATCHED AND 
				(
					dbo.eq(room.STEADGUID,temproom.STEADGUID)=0
					OR dbo.eq(room.NUMBER,temproom.NUMBER)=0
					OR dbo.eq(room.REGIONCODE,temproom.REGIONCODE)=0
					OR dbo.eq(room.POSTALCODE,temproom.POSTALCODE)=0
					OR dbo.eq(room.IFNSFL,temproom.IFNSFL)=0
					OR dbo.eq(room.TERRIFNSFL,temproom.TERRIFNSFL)=0
					OR dbo.eq(room.IFNSUL,temproom.IFNSUL)=0
					OR dbo.eq(room.TERRIFNSUL,temproom.TERRIFNSUL)=0
					OR dbo.eq(room.OKATO,temproom.OKATO)=0
					OR dbo.eq(room.OKTMO,temproom.OKTMO)=0
					OR dbo.eq(room.UPDATEDATE,temproom.UPDATEDATE)=0
					OR dbo.eq(room.PARENTGUID,temproom.PARENTGUID)=0
					OR dbo.eq(room.PREVID,temproom.PREVID)=0
					OR dbo.eq(room.NEXTID,temproom.NEXTID)=0
					OR dbo.eq(room.OPERSTATUS,temproom.OPERSTATUS)=0
					OR dbo.eq(room.STARTDATE,temproom.STARTDATE)=0
					OR dbo.eq(room.ENDDATE,temproom.ENDDATE)=0
					OR dbo.eq(room.NORMDOC,temproom.NORMDOC)=0
					OR dbo.eq(room.LIVESTATUS,temproom.LIVESTATUS)=0
					OR dbo.eq(room.CADNUM,temproom.CADNUM)=0
					OR dbo.eq(room.DIVTYPE,temproom.DIVTYPE)=0
					OR dbo.eq(room.COUNTER,temproom.COUNTER)=0
				)
				THEN UPDATE SET
					STEADGUID = temproom.STEADGUID,
					NUMBER = temproom.NUMBER,
					REGIONCODE = temproom.REGIONCODE,
					POSTALCODE = temproom.POSTALCODE,
					IFNSFL = temproom.IFNSFL,
					TERRIFNSFL = temproom.TERRIFNSFL,
					IFNSUL = temproom.IFNSUL,
					TERRIFNSUL = temproom.TERRIFNSUL,
					OKATO = temproom.OKATO,
					OKTMO = temproom.OKTMO,
					UPDATEDATE = temproom.UPDATEDATE,
					PARENTGUID = temproom.PARENTGUID,
					PREVID = temproom.PREVID,
					NEXTID = temproom.NEXTID,
					OPERSTATUS = temproom.OPERSTATUS,
					STARTDATE = temproom.STARTDATE,
					ENDDATE = temproom.ENDDATE,
					NORMDOC = temproom.NORMDOC,
					LIVESTATUS = temproom.LIVESTATUS,
					CADNUM = temproom.CADNUM,
					DIVTYPE = temproom.DIVTYPE,
					COUNTER = temproom.COUNTER
			WHEN NOT MATCHED 
				THEN 
					INSERT ( 
							STEADGUID,
							NUMBER,
							REGIONCODE,
							POSTALCODE,
							IFNSFL,
							TERRIFNSFL,
							IFNSUL,
							TERRIFNSUL,
							OKATO,
							OKTMO,
							UPDATEDATE,
							PARENTGUID,
							STEADID,
							PREVID,
							NEXTID,
							OPERSTATUS,
							STARTDATE,
							ENDDATE,
							NORMDOC,
							LIVESTATUS,
							CADNUM,
							DIVTYPE,
							COUNTER
							)
					VALUES(
							temproom.STEADGUID,
							temproom.NUMBER,
							temproom.REGIONCODE,
							temproom.POSTALCODE,
							temproom.IFNSFL,
							temproom.TERRIFNSFL,
							temproom.IFNSUL,
							temproom.TERRIFNSUL,
							temproom.OKATO,
							temproom.OKTMO,
							temproom.UPDATEDATE,
							temproom.PARENTGUID,
							temproom.STEADID,
							temproom.PREVID,
							temproom.NEXTID,
							temproom.OPERSTATUS,
							temproom.STARTDATE,
							temproom.ENDDATE,
							temproom.NORMDOC,
							temproom.LIVESTATUS,
							temproom.CADNUM,
							temproom.DIVTYPE,
							temproom.COUNTER
							)
			OUTPUT inserted.STEADID INTO @Upserted(ID);
			DELETE FROM #Stead WHERE STEADID IN (SELECT ID FROM  @Upserted)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT N'Ошибка при обновлении ROOM'
		ROLLBACK TRANSACTION
	END CATCH
