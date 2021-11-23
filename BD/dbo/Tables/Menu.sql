CREATE TABLE [dbo].[Menu] (
    [MenuID]          INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]          VARCHAR (50)  NULL,
    [Descripcion]     VARCHAR (150) NULL,
    [RestauranteID] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([MenuID] ASC),
    CONSTRAINT [FK_Menu_Restaurante] FOREIGN KEY ([RestauranteID]) REFERENCES [dbo].[Restaurante] ([RestauranteID])
);

