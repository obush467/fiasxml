declare @t [dbo].[REGIONCODES]
insert into @t ([REGIONCODE]) values ('77'),('50')
DECLARE @upserted TABLE (AOID uniqueidentifier)
MERGE address.AddressCached AS o
  USING
    (SELECT distinct [AddressID]
           ,[AddressGUID]
           ,[AddressPARENTGUID]
           ,[ItemAddress]
           ,[FullAddress]
           ,[ItemCategory]
           ,[ItemType]
           ,[STARTDATE]
           ,[ENDDATE] 
		   ,regioncode
		   FROM [fias].[AllAddressObjects_withFull] (', '  ,1  ,@t)) tmpo
  ON o.addressID=tmpo.addressID
  WHEN MATCHED AND 
		(dbo.eq(o.[AddressID],tmpo.[AddressID])=0
           or dbo.eq(o.[AddressGUID],tmpo.[AddressGUID])=0
           or dbo.eq(o.[AddressPARENTGUID],tmpo.[AddressPARENTGUID])=0
           or dbo.eq(o.[ItemAddress],tmpo.[ItemAddress])=0
           or dbo.eq(o.[FullAddress],tmpo.[FullAddress])=0
           or dbo.eq(o.[ItemCategory],tmpo.[ItemCategory])=0
           or dbo.eq(o.[ItemType],tmpo.[ItemType])=0
           or dbo.eq(o.[STARTDATE],tmpo.[STARTDATE])=0
           or dbo.eq(o.[ENDDATE],tmpo.[ENDDATE])=0
		   or dbo.eq(o.regioncode,tmpo.regioncode)=0
		   )
		   THEN UPDATE SET 
		   		[AddressID]=tmpo.[AddressID],
           [AddressGUID]=tmpo.[AddressGUID],
           [AddressPARENTGUID]=tmpo.[AddressPARENTGUID],
           [ItemAddress]=tmpo.[ItemAddress],
           [FullAddress]=tmpo.[FullAddress],
           [ItemCategory]=tmpo.[ItemCategory],
		   [ItemType]=tmpo.[ItemType],
           [STARTDATE]=tmpo.[STARTDATE],
           [ENDDATE]=tmpo.[ENDDATE],
		   regioncode=tmpo.regioncode
		   when not matched by source then delete
		   WHEN NOT MATCHED
		   THEN
INSERT ([AddressID]
           ,[AddressGUID]
           ,[AddressPARENTGUID]
           ,[ItemAddress]
           ,[FullAddress]
           ,[ItemCategory]
           ,[ItemType]
           ,[STARTDATE]
           ,[ENDDATE]
		   ,regioncode)
		   values (tmpo.[AddressID]
           ,tmpo.[AddressGUID]
           ,tmpo.[AddressPARENTGUID]
           ,tmpo.[ItemAddress]
           ,tmpo.[FullAddress]
           ,tmpo.[ItemCategory]
           ,tmpo.[ItemType]
           ,tmpo.[STARTDATE]
           ,tmpo.[ENDDATE]
		   ,tmpo.regioncode)		   
		   OUTPUT inserted.addressID INTO @Upserted(AOID);




