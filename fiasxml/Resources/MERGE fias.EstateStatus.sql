DECLARE @upserted TABLE (ID int)
MERGE fias.EstateStatus AS o
  USING
    fias_tmp.EstateStatus tmpo
  ON o.ESTSTATID=tmpo.ESTSTATID
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
				ESTSTATID,NAME,SHORTNAME
				)
        VALUES
          (
            tmpo.ESTSTATID
            ,tmpo.[NAME]
			,tmpo.SHORTNAME         
          )
OUTPUT inserted.ESTSTATID INTO @Upserted(ID);
delete from fias_tmp.EstateStatus where ESTSTATID in (select ID from @upserted)