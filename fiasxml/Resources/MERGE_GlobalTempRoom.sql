WHILE (EXISTS (SELECT * FROM ##Room))
	BEGIN TRY
		BEGIN TRANSACTION
		DECLARE @upserted TABLE (ID uniqueidentifier)
			MERGE fias.Room room
				USING (SELECT DISTINCT * FROM (SELECT TOP 200000 * FROM ##Room) TTT) temproom
				ON room.ROOMID=temproom.ROOMID
			WHEN MATCHED AND 
				(
					dbo.eq(room.ROOMGUID,temproom.ROOMGUID)=0
					OR dbo.eq(room.FLATNUMBER,temproom.FLATNUMBER)=0
					OR dbo.eq(room.FLATTYPE,temproom.FLATTYPE)=0
					OR dbo.eq(room.ROOMNUMBER,temproom.ROOMNUMBER)=0
					OR dbo.eq(room.ROOMTYPE,temproom.ROOMTYPE)=0
					OR dbo.eq(room.REGIONCODE,temproom.REGIONCODE)=0
					OR dbo.eq(room.POSTALCODE,temproom.POSTALCODE)=0
					OR dbo.eq(room.UPDATEDATE,temproom.UPDATEDATE)=0
					OR dbo.eq(room.PREVID,temproom.PREVID)=0
					OR dbo.eq(room.NEXTID,temproom.NEXTID)=0
					OR dbo.eq(room.LIVESTATUS,temproom.LIVESTATUS)=0
					OR dbo.eq(room.OPERSTATUS,temproom.OPERSTATUS)=0
					OR dbo.eq(room.HOUSEGUID,temproom.HOUSEGUID)=0
					OR dbo.eq(room.CADNUM,temproom.CADNUM)=0
					OR dbo.eq(room.STARTDATE,temproom.STARTDATE)=0
					OR dbo.eq(room.ENDDATE,temproom.ENDDATE)=0
					OR dbo.eq(room.ROOMCADNUM,temproom.ROOMCADNUM)=0
					OR dbo.eq(room.NORMDOC,temproom.NORMDOC)=0
				)
				THEN UPDATE SET
					ROOMGUID = temproom.ROOMGUID,
					FLATNUMBER = temproom.FLATNUMBER,
					FLATTYPE = temproom.FLATTYPE,
					ROOMNUMBER = temproom.ROOMNUMBER,
					ROOMTYPE = temproom.ROOMTYPE,
					REGIONCODE = temproom.REGIONCODE,
					POSTALCODE = temproom.POSTALCODE,
					UPDATEDATE = temproom.UPDATEDATE,
					PREVID = temproom.PREVID,
					NEXTID = temproom.NEXTID,
					LIVESTATUS = temproom.LIVESTATUS,
					OPERSTATUS = temproom.OPERSTATUS,
					HOUSEGUID = temproom.HOUSEGUID,
					STARTDATE = temproom.STARTDATE,
					ENDDATE = temproom.ENDDATE,
					ROOMCADNUM = temproom.ROOMCADNUM,
					NORMDOC = temproom.NORMDOC,
					CADNUM = temproom.CADNUM
			WHEN NOT MATCHED 
				THEN 
					INSERT ( 
						ROOMID,
						ROOMGUID,
						FLATNUMBER,
						FLATTYPE,
						ROOMNUMBER,
						ROOMTYPE ,
						REGIONCODE,
						POSTALCODE ,
						UPDATEDATE,
						PREVID,
						NEXTID,
						LIVESTATUS,
						OPERSTATUS,
						HOUSEGUID,
						STARTDATE,
						ENDDATE,
						ROOMCADNUM,
						NORMDOC,
						CADNUM)
					VALUES(
						temproom.ROOMID,
						temproom.ROOMGUID,
						temproom.FLATNUMBER,
						temproom.FLATTYPE,
						temproom.ROOMNUMBER,
						temproom.ROOMTYPE,
						temproom.REGIONCODE,
						temproom.POSTALCODE,
						temproom.UPDATEDATE,
						temproom.PREVID,
						temproom.NEXTID,
						temproom.LIVESTATUS,
						temproom.OPERSTATUS,
						temproom.HOUSEGUID,
						temproom.STARTDATE,
						temproom.ENDDATE,
						temproom.ROOMCADNUM,
						temproom.NORMDOC,
						temproom.CADNUM)
			OUTPUT inserted.ROOMID INTO @Upserted(ID);
			DELETE FROM ##Room WHERE ROOMID IN (SELECT ID FROM  @Upserted)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT N'Ошибка при обновлении ROOM'
		ROLLBACK TRANSACTION
	END CATCH
