DECLARE @upserted TABLE (ID int)
MERGE fias.NormativeDocumentType AS o
  USING
    ##NormativeDocumentType tmpo
  ON o.NDTYPEID=tmpo.NDTYPEID
  WHEN MATCHED AND 
    (
      dbo.eq(o.NAME,tmpo.NAME)=0
	)
  THEN UPDATE SET 
		NAME=tmpo.NAME
  WHEN NOT MATCHED
  THEN  INSERT (
            NDTYPEID,NAME
)
        VALUES
          (
            tmpo.NDTYPEID
            ,tmpo.[NAME]         
          )
OUTPUT inserted.NDTYPEID INTO @Upserted(ID);
delete from ##NormativeDocumentType