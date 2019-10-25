DECLARE @upserted TABLE (ID int)
MERGE fias.RoomType AS o
  USING
    #RoomType tmpo
  ON o.RMTYPEID=tmpo.RMTYPEID
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
            RMTYPEID,NAME,SHORTNAME
)
        VALUES
          (
            tmpo.RMTYPEID
            ,tmpo.[NAME]
			,tmpo.SHORTNAME          
          )
OUTPUT inserted.RMTYPEID INTO @Upserted(ID);
delete from #RoomType 