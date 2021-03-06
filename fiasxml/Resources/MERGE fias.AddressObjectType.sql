MERGE fias.AddressObjectType AS o
  USING
    fias_tmp.AddressObjectType tmpo
  ON o.LEVEL=tmpo.LEVEL and o.KOD_T_ST=tmpo.KOD_T_ST
  WHEN MATCHED AND 
    (
      dbo.eq(o.SOCRNAME,tmpo.SOCRNAME)=0
	  AND 
	  dbo.eq(o.SCNAME,tmpo.SCNAME)=0
  )
  THEN UPDATE SET 
		SOCRNAME=tmpo.SOCRNAME,
		KOD_T_ST=tmpo.KOD_T_ST
  WHEN NOT MATCHED
  THEN  INSERT (
            [LEVEL]
      ,[SCNAME]
      ,[SOCRNAME]
      ,[KOD_T_ST]
)
        VALUES
          (tmpo.[LEVEL],tmpo.[SCNAME],tmpo.[SOCRNAME],tmpo.[KOD_T_ST]);
delete from fias_tmp.AddressObjectType