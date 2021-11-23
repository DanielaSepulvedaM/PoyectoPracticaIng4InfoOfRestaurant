CREATE TABLE [dbo].[Restaurante] (
    [RestauranteID] INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]        VARCHAR (100) NOT NULL,
    [Nit]           VARCHAR (15)  NULL,
    [Telefono]      VARCHAR (10)  NULL,
    [Direccion]     VARCHAR (100) NULL,
    [Barrio]        VARCHAR (50)  NULL,
    [Eliminado]     BIT           NULL,
    PRIMARY KEY CLUSTERED ([RestauranteID] ASC)
);

