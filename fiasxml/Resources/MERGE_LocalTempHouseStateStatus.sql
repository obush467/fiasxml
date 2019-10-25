DECLARE @upserted TABLE (ID int)
MERGE fias.HouseStateStatus AS o
  USING
    #HouseStateStatus tmpo
  ON o.HOUSESTID=tmpo.HOUSESTID
  WHEN MATCHED AND 
    (
      dbo.eq(o.NAME,tmpo.NAME)=0
	)
  THEN UPDATE SET 
		NAME=tmpo.NAME
  WHEN NOT MATCHED
  THEN  INSERT (
            HOUSESTID,NAME
)
        VALUES
          (
            tmpo.HOUSESTID
            ,tmpo.[NAME]         
          )
OUTPUT inserted.HOUSESTID INTO @Upserted(ID);
delete from #HouseStateStatus