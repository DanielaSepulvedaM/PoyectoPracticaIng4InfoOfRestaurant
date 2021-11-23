/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [Restaurante] AS TARGET
USING (VALUES
		    ('Carnes y Sopitas','8600482590','2698532','cll 22 # 30 - 64', 'Chapinero',0),
            ('Bon Market and Bar','8286004590','5322698','cr 25 # 60 - 64', 'Galerias',0),
            ('Churrasqueria','8259086004','8526932','diagonal 30 - 64', 'centro',0),
            ('Osaka Cocina','6800482590','9853262','cll 1 # 2 - 15', 'Candelaria',0),
            ('Capital Cocina','8860042590','6985322','local 125 cc', 'zona T',0),
            ('Comida Peruana','9086004825','2269853','cr 3 # 30 - 64', 'centro',0))
AS SOURCE (Nombre,Nit,Telefono,Direccion,Barrio,Eliminado)
ON TARGET.Nit = SOURCE.Nit
WHEN NOT MATCHED THEN 
    INSERT (Nombre,Nit,Telefono,Direccion,Barrio,Eliminado)
    VALUES (Nombre,Nit,Telefono,Direccion,Barrio,Eliminado);