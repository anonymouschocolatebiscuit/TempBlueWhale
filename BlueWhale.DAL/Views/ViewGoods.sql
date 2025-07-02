SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--drop view viewGoods                  
create view [dbo].[viewGoods]                  
as    
                  
select a.*,                  
s.names shopName,                  
u.names unitName,                  
ck.names ckName,                  
t.names typeName,            
p.names brandName,                  
sumNumStart,                  
--sumPriceStart/sumNumStart costUnit,                  
case when sumNumStart=0 then 0 else cast(round(sumPriceStart/sumNumStart,3)   as   numeric(20,3)) end          
--cast(round(sumPriceStart/sumNumStart,3)   as   numeric(20,3))          
 costUnit,            
          
--priceCost costUnit,          
            
sumPriceStart,    
isnull(imagesPath,'nopic.jpg') imagesPathMoren --default image                 
                  
from goods a                  
left join viewGoodsNumStart b on a.id=b.goodsId --initial quantity                  
left join shop s on a.shopId=s.id --shop                 
left join unit u on a.unitId=u.id --unit                  
left join inventory ck on a.ckId=ck.id --inventory                  
left join goodsType t on a.typeId=t.id --type            
left join goodsBrand p on a.brandId=p.id --brand     
    
left join goodsImages img on a.id=img.goodsId and img.moren=1
GO