DECLARE @upserted TABLE (ID int)
MERGE fias.OperationStatus AS o
  USING
    ##OperationStatus tmpo
  ON o.OPERSTATID=tmpo.OPERSTATID
  WHEN MATCHED AND 
    (
      dbo.eq(o.NAME,tmpo.NAME)=0
	)
  THEN UPDATE SET 
		NAME=tmpo.NAME
  WHEN NOT MATCHED
  THEN  INSERT (
            OPERSTATID,NAME
)
        VALUES
          (
            tmpo.OPERSTATID
            ,tmpo.[NAME]         
          )
OUTPUT inserted.OPERSTATID INTO @Upserted(ID);
delete from ##OperationStatus