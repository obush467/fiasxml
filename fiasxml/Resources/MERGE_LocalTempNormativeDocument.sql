WHILE (EXISTS (SELECT * FROM #NormativeDocument))
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @upserted TABLE (ID uniqueidentifier)
			MERGE fias.NormativeDocument nd
				USING (SELECT DISTINCT * FROM (SELECT TOP 10000 * FROM #NormativeDocument) TTT) tempnd
				ON nd.NORMDOCID=tempnd.NORMDOCID
			WHEN MATCHED AND 
				(
					dbo.eq(nd.DOCIMGID,tempnd.DOCIMGID)=0
					OR dbo.eq(nd.[DOCNAME],tempnd.DOCNAME)=0
					OR dbo.eq(nd.[DOCDATE],tempnd.DOCDATE)=0
					OR dbo.eq(nd.[DOCNUM],tempnd.DOCNUM)=0
					OR dbo.eq(nd.[DOCTYPE],tempnd.DOCTYPE)=0
				)
				THEN UPDATE SET
					DOCIMGID=tempnd.DOCIMGID,
					DOCNAME=tempnd.DOCNAME,
					DOCDATE=tempnd.DOCDATE,
					DOCNUM=tempnd.DOCNUM,
					DOCTYPE=tempnd.DOCTYPE
			WHEN NOT MATCHED 
				THEN 
					INSERT (
						NORMDOCID,
						DOCIMGID,
						DOCNAME,
						DOCDATE,
						DOCNUM,
						DOCTYPE)							
					VALUES(
						tempnd.NORMDOCID,
						tempnd.DOCIMGID,
						tempnd.DOCNAME,
						tempnd.DOCDATE,
						tempnd.DOCNUM,
						tempnd.DOCTYPE
							
							)
			OUTPUT inserted.NORMDOCID INTO @Upserted(ID);
			DELETE FROM #NormativeDocument WHERE NORMDOCID  IN (SELECT ID FROM  @Upserted)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT N'Ошибка при обновлении nd'
		ROLLBACK TRANSACTION
	END CATCH
