DECLARE @upserted TABLE (ID int)
MERGE fias.ActualStatus AS o
  USING
    #ActualStatus tmpo
  ON o.ACTSTATID=tmpo.ACTSTATID
  WHEN MATCHED AND 
    (
      dbo.eq(o.NAME,tmpo.NAME)=0

  )
  THEN UPDATE SET 
		NAME=tmpo.NAME
  WHEN NOT MATCHED
  THEN  INSERT ([ACTSTATID],[NAME])
        VALUES
          (tmpo.[ACTSTATID],tmpo.[NAME])
OUTPUT inserted.[ACTSTATID] INTO @Upserted(ID);
delete from #ActualStatus