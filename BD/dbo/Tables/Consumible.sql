CREATE TABLE [dbo].[Consumible] (
    [ConsumibleID]     INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]      VARCHAR (50)    NOT NULL,
    [Descripcion] VARCHAR (150)   NOT NULL,
    [Precio]      DECIMAL (12, 2) NOT NULL,
    [Imagen]  IMAGE   NULL,
    [RestauranteID]    INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([ConsumibleID] ASC),
    CONSTRAINT [FK_Consumible_Restaurante] FOREIGN KEY ([RestauranteID]) REFERENCES [dbo].[Restaurante] ([RestauranteID])
);

