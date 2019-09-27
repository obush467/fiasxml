DECLARE @upserted TABLE (ID int)
MERGE fias.NormativeDocumentType AS o
  USING
    fias_tmp.NormativeDocumentType tmpo
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
delete from fias_tmp.NormativeDocumentType where NDTYPEID in (select ID from @upserted)