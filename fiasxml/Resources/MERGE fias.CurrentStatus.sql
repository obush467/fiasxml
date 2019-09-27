DECLARE @upserted TABLE (ID int)
MERGE fias.CurrentStatus AS o
  USING
    fias_tmp.CurrentStatus tmpo
  ON o.CURENTSTID=tmpo.CURENTSTID
  WHEN MATCHED AND 
    (
      dbo.eq(o.NAME,tmpo.NAME)=0

  )
  THEN UPDATE SET 
		NAME=tmpo.NAME
  WHEN NOT MATCHED
  THEN  INSERT (
            CURENTSTID,[NAME]
)
        VALUES
          (
            tmpo.CURENTSTID
            ,tmpo.[NAME]
          
          )
OUTPUT inserted.CURENTSTID INTO @Upserted(ID);
delete from fias_tmp.CurrentStatus where CURENTSTID in (select ID from @upserted)