CREATE TABLE [dbo].[ConsumibleMenu]
(
	[ConsumibleMenuID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ConsumibleID] INT NOT NULL,
	[MenuID] INT NOT NULL,
	CONSTRAINT FK_ConsumibleMenu_Menu FOREIGN KEY ([MenuID]) REFERENCES Menu ([MenuID]) ON DELETE CASCADE, 
	CONSTRAINT FK_ConsumibleMenu_Consumible FOREIGN KEY ([ConsumibleID]) REFERENCES Consumible ([ConsumibleID])
)
