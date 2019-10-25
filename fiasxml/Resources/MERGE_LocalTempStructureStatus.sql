DECLARE @upserted TABLE (ID int)
MERGE fias.StructureStatus AS o
  USING
    #StructureStatus tmpo
  ON o.STRSTATID=tmpo.STRSTATID
  WHEN MATCHED AND 
    (
      dbo.eq(o.NAME,tmpo.NAME)=0
	  OR
	  dbo.eq(o.SHORTNAME,tmpo.SHORTNAME)=0
  )
  THEN UPDATE SET 
		NAME=tmpo.NAME
  WHEN NOT MATCHED
  THEN  INSERT (
            STRSTATID,NAME,SHORTNAME
)
        VALUES
          (
            tmpo.STRSTATID
            ,tmpo.[NAME]
			,tmpo.SHORTNAME          
          )
OUTPUT inserted.STRSTATID INTO @Upserted(ID);
delete from #StructureStatus