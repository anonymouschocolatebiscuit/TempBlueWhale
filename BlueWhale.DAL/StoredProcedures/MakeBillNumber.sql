USE [BlueWhale_ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE IF EXISTS [dbo].[makeBillNumber]
GO

CREATE PROCEDURE [dbo].[makeBillNumber]        
(          
    @shopId         INT,
    @tableName      VARCHAR(100),        
    @NumberHeader   NVARCHAR(40)   
)       
AS          
DECLARE @sql VARCHAR(1000)          
SET @sql='           
DECLARE @newOrderNo varchar(20);       
SELECT  @newOrderNo =   (            
                        SELECT TOP 1 number           
                        FROM '+@tableName +'           
                        WHERE CONVERT(VARCHAR(8),GETDATE(),112)=SUBSTRING(NUMBER,6,8)   
                        AND shopId='''+CAST(@shopId AS VARCHAR(100))+'''         
                        ORDER BY number DESC
                        )    
          
ID (@newOrderNo IS NULL)             
  BEGIN            
   SET @newOrderNo='''+@NumberHeader+'''+''-''+CONVERT(VARCHAR(8),GETDATE(),112)+'+'''-0001'''+ '          
  END            
ELSR            
  BEGIN            
   SET @newOrderNo=SUBSTRING(@newOrderNo,1,14)+SUBSTRING(CONVERT(VARCHAR(20),(CONVERT(INT,SUBSTRING(@newOrderNo,15,4))+10001)),2,4)          
  END   
  
SELECT @newOrderNo AS BillNumer '          
              
exec(@sql)          

