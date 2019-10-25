DECLARE @upserted TABLE (ID int)
MERGE fias.CenterStatus AS o
  USING
    fias_tmp.CenterStatus tmpo
  ON o.CENTERSTID=tmpo.CENTERSTID
  WHEN MATCHED AND 
    (
      dbo.eq(o.NAME,tmpo.NAME)=0

  )
  THEN UPDATE SET 
		NAME=tmpo.NAME
  WHEN NOT MATCHED
  THEN  INSERT (
            [CENTERSTID],[NAME]
)
        VALUES
          (
            tmpo.[CENTERSTID]
            ,tmpo.[NAME]
          
          )
OUTPUT inserted.[CENTERSTID] INTO @Upserted(ID);
delete from fias_tmp.CenterStatus